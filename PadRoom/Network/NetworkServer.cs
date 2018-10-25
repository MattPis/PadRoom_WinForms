using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PadRoom.Network
{

    class NetworkServer
    {

        private Socket _serverSocket;
        private List<Socket> _clientSockets = new List<Socket>();

        private byte[] _buffer = new byte[1024];
        private readonly int port = 6788;

        private ManualResetEvent connectDone =
           new ManualResetEvent(false);

        public static event EventHandler MessageRecievedHandler;

        public void StartServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                _serverSocket.Listen(1);

                while (true)
                {
                    connectDone.Reset();
                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                    connectDone.WaitOne();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                _serverSocket.Shutdown(SocketShutdown.Both);
                _serverSocket.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        public bool IsConnected()
        {
            return _clientSockets.Count > 0;
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _clientSockets.Add(socket);
            connectDone.Set();
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void RecieveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int recieved = socket.EndReceive(AR);

            if (recieved > 0)
            {
                byte[] dataBuf = new byte[recieved];
                Array.Copy(_buffer, dataBuf, recieved);
                OnMessageRecieved(dataBuf);
            }

            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallback), socket);
        }

        private void OnMessageRecieved(byte[] buffer)
        {
            if (MessageRecievedHandler != null)
            {
                MessageRecievedHandler(buffer, EventArgs.Empty);
            }
        }

        public void Send(byte[] buffer)
        {
            try
            {
                foreach (Socket socket in _clientSockets)
                {
                    socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
        }

        public void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallback), socket);
        }

    }
}
