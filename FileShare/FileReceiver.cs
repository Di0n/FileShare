//#define USE_LOCAL

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _Timer = System.Timers.Timer;

namespace FileShare
{
    class FileReceiver : IDisposable
    {
        public delegate void FileTransferRequestHandler(object sender, FileTransferRequestEventArgs args);
        public delegate void FileTransferProgressHandler(object sender, FileTransferProgressEventArgs args);
        public delegate void FileTransferCancelledHandler(object sender, EventArgs args);

        public event FileTransferRequestHandler FileTransferRequest;
        public event FileTransferProgressHandler FileTransferProgressChanged;
        public event FileTransferCancelledHandler FileTransferCancelled;

        private Computer computer;
        private ManualResetEvent resetEvent;
        private IPEndPoint endPoint;

        private const char NT = '\0';
        private const string FT_REQUEST_HEADER = "FT_REQUEST";
        private const string FT_RESPONSE_HEADER = "FT_RESPONSE";

        public FileReceiver() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (resetEvent != null)
                {
                    resetEvent.Dispose();
                }
            }
        }

        /// <summary>
        /// Runs the FileReceiver.
        /// This method is blocking.
        /// </summary>
        public void Run()
        {
#if USE_LOCAL
            computer = new Computer() { IP = "127.0.0.1", Name = Environment.MachineName };
#else
            computer = new Computer() { IP = Utility.GetLocalIPAddress(), Name = Environment.MachineName };
#endif
            endPoint = new IPEndPoint(IPAddress.Parse(computer.IP), computer.Port);
            resetEvent = new ManualResetEvent(false);

            using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                listener.Bind(endPoint);
                listener.Listen(10);

                while (true)
                {
                    resetEvent.Reset();

                    listener.BeginAccept(new AsyncCallback(ConnectCallback), listener);
                    // Wait for a connection
                    resetEvent.WaitOne();
                }
            }
        }

        /// <summary>
        /// Connection callback.
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            Socket handler = null;

            resetEvent.Set();

            Socket _listener = (Socket)ar.AsyncState;

            try
            {
                handler = _listener.EndAccept(ar);
            }
            catch (SocketException)
            {
                // Connection failed. Keep program going.
                if (handler != null)
                    handler.Dispose();
                return;
            }

            MessageStateObject state = new MessageStateObject() { WorkSocket = handler };
            handler.BeginReceive(state.Buffer, 0, MessageStateObject.BufferSize, 0, new AsyncCallback(ReceiveMsgCallback), state);
        }

        /// <summary>
        /// Callback message receiver.
        /// </summary>
        /// <param name="ar"></param>
        private async void ReceiveMsgCallback(IAsyncResult ar)
        {
            MessageStateObject state = (MessageStateObject)ar.AsyncState;
            Socket handler = state.WorkSocket;

            int bytesRead = 0;
            try
            {
                bytesRead = handler.EndReceive(ar);
            }
            catch (SocketException)
            {
                handler.Dispose();
                return;
            }

            if (bytesRead > 0)
            {
                state.Builder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));
                string content = state.Builder.ToString();

                if (content.IndexOf(NT) == -1)
                {
                    handler.BeginReceive(state.Buffer, 0, MessageStateObject.BufferSize, 0, new AsyncCallback(ReceiveMsgCallback), state);
                    return;
                }

                if (content.StartsWith(FT_REQUEST_HEADER))
                {
                    try
                    {
                        await HandleFileRequestAsync(state, content);
                    }
                    catch (InvalidDataException)
                    {
                        // invalid packet received
                        handler.Dispose();
                    }
                }
                else
                {
                    // invalid/ no header, don't accept this connection.
                    handler.Dispose();
                }
            }
            else
            {
                handler.Dispose();
            }
        }

        /// <summary>
        /// Handles the file transfer request
        /// If the transfer is accepted the program will receive the file.
        /// If the transfer gets declined the conneciton with the client will be closed.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task HandleFileRequestAsync(MessageStateObject state, string request)
        {
            // A File transfer request packet looks like this
            // "FT_REQUEST[<computer_name>\r\nfilename\r\nfilesize]\0"
            string computerName, fileName, fileSizeStr;
            try
            {
                request = request.Substring(FT_REQUEST_HEADER.Length + 1);
                request = request.Remove(request.IndexOf(']'));

                char[] delimiters = new char[] { '\r', '\n' };
                string[] parts = request.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                computerName = parts[0];
                fileName = parts[1];
                fileSizeStr = parts[2];
            }
            catch (ArgumentException ex) { throw new InvalidDataException("Invalid File transfer request data.", ex); }

            long fileSize;

            if (!long.TryParse(fileSizeStr, out fileSize))
                throw new InvalidDataException("Filesize is not a valid number.");

            Socket handler = state.WorkSocket;
            Computer sender = new Computer() { Name = computerName, IP = (handler.RemoteEndPoint as IPEndPoint).Address.ToString() };

            bool acceptTransfer = await Task.Run<bool>(() =>
            {
                FileTransferRequestEventArgs args = new FileTransferRequestEventArgs(sender, fileName, fileSize);
                OnFileTransferRequest(args);
                return args.Accept;
            });

            // A File transfer response packet looks like this:
            // "FT_RESPONSE[<computer_name>\r\nACCEPT|DECLINE]\0"

            string response = String.Format("{0}[{1}\r\n{2}]", FT_RESPONSE_HEADER, computer.Name, acceptTransfer ? "ACCEPT" : "DECLINE");
            FileStateObject fso = new FileStateObject() { WorkSocket = handler, BytesToReceive = fileSize };

            BeginSend(handler, response, (acceptTransfer) ? (new AsyncCallback(AcceptTransferCallback)) : (new AsyncCallback(DeclineTransferCallback)), fso);
        }

        /// <summary>
        /// Gets called when the user sent the decline message to the target computer.
        /// </summary>
        /// <param name="ar"></param>
        private void DeclineTransferCallback(IAsyncResult ar)
        {
            FileStateObject fso = (FileStateObject)ar.AsyncState;
            Socket handler = fso.WorkSocket;

            try
            {
                int bytesSent = handler.EndSend(ar);
                Debug.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (SocketException) { }

            if (handler != null)
            {
                try
                {
                    handler.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException) { }
                handler.Dispose();
            }
            fso.Dispose();
        }
        /// <summary>
        /// Gets called when the user sent the accept message to the target computer.
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptTransferCallback(IAsyncResult ar)
        {
            FileStateObject fso = (FileStateObject)ar.AsyncState;
            Socket handler = fso.WorkSocket;

            try
            {
                int bytesSent = handler.EndSend(ar);
                Debug.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (SocketException)
            {
                if (handler != null)
                {
                    handler.Dispose();
                }
                fso.Dispose();
                return;
            }

            handler.BeginReceive(fso.Buffer, 0, FileStateObject.BufferSize, 0, new AsyncCallback(ReceiveFileCallback), fso);
        }

        /// <summary>
        /// Gets called after every Receive().
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveFileCallback(IAsyncResult ar)
        {
            FileStateObject state = (FileStateObject)ar.AsyncState;
            Socket handler = state.WorkSocket;

            int bytesRead = 0;
            try
            {
                bytesRead = handler.EndReceive(ar); // ConnectionAborted
            }
            catch (SocketException)
            {
                handler.Dispose();
                state.Dispose();
                return;
            }

            if (bytesRead > 0)
            {
                state.BytesReceived += bytesRead;

                int transferSpeed = 0; // speed in kbps

                if (state.TransferSpeedSw == null)
                {
                    state.TransferSpeedSw = new Stopwatch();
                }
                else
                {
                    state.TransferSpeedSw.Stop();
                    if (state.TransferSpeedSw.ElapsedMilliseconds != 0)
                    {
                        transferSpeed = (int)((bytesRead * 8) / state.TransferSpeedSw.ElapsedMilliseconds);
                    }
                }

                FileTransferProgressEventArgs args = new FileTransferProgressEventArgs(state.BytesReceived, state.BytesToReceive, transferSpeed);
                OnFileTransferProgressChanged(args);

                if (args.Cancel)
                {
                    state.Dispose();
                    handler.Dispose();
                    return;
                }

                state.Output = state.Output ?? new Func<FileStream>(() =>
                {
                    string fileName = String.Format("{0}.zip", DateTime.UtcNow.ToFileTimeUtc().ToString());

                    string downloadPath = KnownFolders.GetPath(KnownFolder.Downloads) + "\\";

                    string filePath = downloadPath + fileName;

                    return new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, System.IO.FileShare.None, 4096, true);
                })();

                state.Output.BeginWrite(state.Buffer, 0, bytesRead, new AsyncCallback(new Action<IAsyncResult>((result) =>
                {
                    FileStateObject fso = (FileStateObject)result.AsyncState;
                    fso.Output.EndWrite(result);

                    if (state.BytesToReceive == state.BytesReceived)
                        Debug.WriteLine(String.Format("State output: {0}\nBytesreceived: {1}", state.Output.Length, state.BytesReceived));

                    state.TransferSpeedSw.Reset();
                    state.TransferSpeedSw.Start();

                    fso.WorkSocket.BeginReceive(fso.Buffer, 0, FileStateObject.BufferSize, 0, new AsyncCallback(ReceiveFileCallback), fso);
                })), state);
            }
            else
            { // Done or cancelled
                if (state.BytesReceived != state.BytesToReceive)
                    OnFileTransferCancelled();

                state.Dispose();
                handler.Dispose();
            }
        }

        /// <summary>
        /// Starts sending data to a socket (async)
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        private void BeginSend(Socket handler, string data, AsyncCallback callback, object state)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data + NT);
            handler.BeginSend(byteData, 0, byteData.Length, 0, callback, state);
        }

        /// <summary>
        /// Fires the FileTransferRequest event.
        /// </summary>
        /// <param name="args"></param>
        private void OnFileTransferRequest(FileTransferRequestEventArgs args)
        {
            if (FileTransferRequest != null)
            {
                FileTransferRequest.Invoke(this, args);
            }
        }

        private void OnFileTransferProgressChanged(FileTransferProgressEventArgs args)
        {
            if (FileTransferProgressChanged != null)
            {
                FileTransferProgressChanged(this, args);
            }
        }

        private void OnFileTransferCancelled()
        {
            if (FileTransferCancelled != null)
            {
                FileTransferCancelled(this, EventArgs.Empty);
            }
        }
    }

    //State object for messages.
    class MessageStateObject
    {
        public Socket WorkSocket { get; set; }
        public StringBuilder Builder { get; set; }
        public static int BufferSize { get { return 1024; } }
        public byte[] Buffer { get; set; }

        public MessageStateObject()
        {
            Buffer = new byte[BufferSize];
            Builder = new StringBuilder();
        }
    }

    //State object for file transfers
    class FileStateObject : IDisposable
    {
        public Socket WorkSocket { get; set; }
        public static int BufferSize { get { return 1024000; } } // 1048576, 1024000, 1280000
        public byte[] Buffer { get; set; }
        public FileStream Output { get; set; }
        public long BytesToReceive { get; set; }
        public long BytesReceived { get; set; }
        public Stopwatch TransferSpeedSw { get; set; }

        public FileStateObject()
        {
            Buffer = new byte[BufferSize];
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
                if (Output != null)
                {
                    Output.Dispose();
                    Output = null;
                }
            }
        }
    }
}
