using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using IOFile = System.IO.File;

namespace FileShare
{
    class FileSender : IDisposable
    {
        public delegate void FileSendProgressHandler(object sender, FileSendProgressEventArgs args);
        public event FileSendProgressHandler FileSendProgress;

        private const char NULL_TERMINATOR = '\0';
        private const string FT_REQUEST = "FT_REQUEST";
        private const string FT_RESPONSE = "FT_RESPONSE";
        private const int FILE_BUFFER_SIZE = 1024000; // 1280000,  5120000, 1048576, 1024000
        private Socket socket;
        private Computer receiver;

        public FileSender()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (socket != null)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// Starts connecting to a remote computer.
        /// </summary>
        /// <param name="computer"></param>
        /// <exception cref="SocketException"></exception>
        /// <returns></returns>
        public async Task ConnectAsync(Computer computer, CancellationToken token) // Exception als het niet gelukt is.
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(computer.IP), computer.Port);

            await socket.ConnectTaskAsync(endPoint, token);
            receiver = computer;
        }

        public void Close()
        {
            if (socket == null) return;

            try
            { socket.Shutdown(SocketShutdown.Both); }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
            finally 
            { 
                if (socket != null)
                    socket.Dispose(); 
            }
        }

        /// <summary>
        /// Starts an asynchronous SendText operation.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="SocketException"></exception>
        private async Task SendTextAsync(string text)
        {
            text += NULL_TERMINATOR;
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            int sent = 0;
            do
            {
                sent = await socket.SendTaskAsync(buffer, sent, buffer.Length, SocketFlags.None);
            }
            while (sent != buffer.Length); // If there is still data to be sent, send the remaining data.
        }

        /// <summary>
        /// Receives text from the connected socket.
        /// </summary>
        /// <returns></returns>
        /// /// <exception cref="SocketException"></exception>
        private async Task<string> ReceiveTextAsync(CancellationToken token)
        {
            byte[] buffer = new byte[256];
            int received = await socket.ReceiveTaskAsync(buffer, 0, buffer.Length, SocketFlags.None, token ,15000);
            if (received == 0)
                throw new SocketException((int)SocketError.Disconnecting); // If received == 0 the remote socket closed the connection with shutdown. (graceful)

            string text = Encoding.ASCII.GetString(buffer, 0, received);
            return text;
        }

        /// <summary>
        /// Requests the connected socket to start a file transfer.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SocketException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public async Task<RequestResponse> RequestFileSendAsync(File file, CancellationToken token)
        {
            if (socket == null || !socket.Connected) throw new SocketException();

            string header = String.Format("{0}[{1}\r\n{2}\r\n{3}]", FT_REQUEST, Environment.MachineName, file.Name, file.Size);

            await SendTextAsync(header);
            string response = await ReceiveTextAsync(token);

            RequestResponse resp = new RequestResponse(response);
            resp.Parse();
            return resp;
        }
        
        /// <summary>
        /// Starts a file send operation.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="SocketException"></exception>
        public Task SendFileAsync(File file, CancellationToken cancellationToken)
        {
            if (socket == null || !socket.Connected) throw new Exception();

            return Task.Run(async () => // Temp fix https://stackoverflow.com/questions/44231957/stream-async-read-write-freezing-ui-thread
                {
                    using (NetworkStream networkStream = new NetworkStream(socket, FileAccess.Write, false))
                    using (FileStream fStream = new FileStream(file.Path, FileMode.Open, FileAccess.Read, System.IO.FileShare.None, 8192, FileOptions.Asynchronous))
                    {
                        byte[] buffer = new byte[FILE_BUFFER_SIZE];
                        int read = 0;
                        long sent = 0;

                        while ((read = await fStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) != 0)
                        {
                            await networkStream.WriteAsync(buffer, 0, read, cancellationToken);
                            OnFileSendProgressChange(new FileSendProgressEventArgs(receiver, file.Size, sent += read));
                            if (cancellationToken.IsCancellationRequested)
                                return;
                        }
                        await networkStream.FlushAsync(cancellationToken);
                    }
                });
        }

        private void OnFileSendProgressChange(FileSendProgressEventArgs args)
        {
            if (FileSendProgress != null)
                FileSendProgress(this, args);
        }

        public class RequestResponse
        {
            private string response;
            public RequestResponse(string response)
            {
                this.response = response;
            }

            /// <summary>
            /// Parses the response data.
            /// </summary>
            /// <exception cref="ArgumentException"></exception>
            /// <exception cref="FormatException"></exception>
            public void Parse()
            {
                response = response.Substring(FT_RESPONSE.Length + 1);
                response = response.Remove(response.IndexOf(']'));
                char[] delimiters = new char[] { '\r', '\n' };
                string[] parts = response.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                string computerName = parts[0];
                string accepted = parts[1];

                ComputerName = computerName;
                if (accepted == "ACCEPT") RequestAccepted = true;
                else if (accepted == "DECLINE") RequestAccepted = false;
                else
                {
                    throw new FormatException("Invalid response.");
                }
            }

            public bool RequestAccepted { get; private set; }
            public string ComputerName { get; private set; }
        }
    }
}
