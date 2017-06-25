using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using _Timer = System.Timers.Timer;

namespace FileShare
{
    public static class ExtensionMethods
    {
        // Type extension: Int64
        public static string ToSize(this long value, SizeUnit unit)
        {
            return (value / (double)Math.Pow(1024, (long)unit)).ToString("0.00");
        }

        public static Task ConnectTaskAsync(this Socket socket, EndPoint endPoint)
        {
            return Task.Factory.FromAsync(socket.BeginConnect(endPoint, null, null), socket.EndConnect);
        }

        public static async Task ConnectTaskAsync(this Socket socket, EndPoint endPoint, CancellationToken token)
        {
            using (_Timer checkCancellationTimer = new _Timer(2000))
            {
                checkCancellationTimer.Elapsed += (o, e) =>
                {
                    if (token.IsCancellationRequested)
                        socket.Dispose(); // Microsoft bad design.
                };
                checkCancellationTimer.Start();
                try
                {
                    await Task.Factory.FromAsync(socket.BeginConnect(endPoint, null, null), socket.EndConnect);
                }
                catch (ObjectDisposedException)
                {
                    if (token.IsCancellationRequested)
                        throw new TaskCanceledException("CancellationRequested.");
                    else
                        throw;
                }
            }
        }

        public static Task DisconnectTaskAsync(this Socket socket, bool reuseSocket)
        {
            return Task.Factory.FromAsync(socket.BeginDisconnect(reuseSocket, null, null), socket.EndDisconnect);
        }

        public static Task<int> SendTaskAsync(this Socket socket, byte[] buffer, int offset, int size, SocketFlags flags)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(buffer, offset, size);
            args.SocketFlags = flags;
            args.Completed += (o, e) =>
                {
                    tcs.SetResult(e.BytesTransferred);
                };

            socket.SendAsync(args);

            return tcs.Task;
        }

        public static async Task<int> ReceiveTaskAsync(this Socket socket, byte[] buffer, int offset, int size, SocketFlags flags, CancellationToken token, int timeOut = 0)
        {
            using (_Timer checkCancellationTimer = new _Timer(2000))
            {
                checkCancellationTimer.Elapsed += (o, e) =>
                    {
                        if (token.IsCancellationRequested)
                            socket.Dispose();
                    };
                checkCancellationTimer.Start();

                try
                {
                    return await Task.Run<int>(() =>
                        {
                            socket.ReceiveTimeout = timeOut;
                            return socket.Receive(buffer, offset, size, flags);
                        });
                }
                catch (ObjectDisposedException)
                {
                    if (token.IsCancellationRequested)
                        throw new TaskCanceledException("CancellationRequested");
                    else
                        throw;
                }
            }
        }

        public static bool IsConnected(this Socket socket)
        {
            return !(!socket.Connected || (socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0));
        }

        public static void Clear(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}
