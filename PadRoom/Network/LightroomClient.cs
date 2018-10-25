using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace PadRoom
{

    class LightroomClient
    {
        private Socket _socket;
        private byte[] _buffer = new byte[1024];

        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);

        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public event EventHandler MessageRecievedHandler;

        public void Start(object obj)
        {
            try
            {
                int port = (int)obj;
               
                IPHostEntry ipHostInfo = Dns.Resolve("localHost");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                _socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                _socket.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), _socket);
                connectDone.WaitOne();

                Receive();
                receiveDone.WaitOne();

                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Stop()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        public bool IsConnected()
        {
            return _socket.Connected;
        }

        private void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                _socket.EndConnect(AR);
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive()
        {
            try
            {
                _socket.BeginReceive(_buffer, 0, 256, 0,
                    new AsyncCallback(ReceiveCallback), _socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int bytesRecieved = _socket.EndReceive(AR);

                if (bytesRecieved > 0)
                {
                    byte[] dataBuf = new byte[bytesRecieved];
                    Array.Copy(_buffer, dataBuf, bytesRecieved);
                    Console.WriteLine("Recieved {0} bytes from Lightroom.", bytesRecieved);
                    OnMessageRecieved(dataBuf);
                }
                else
                {
                    receiveDone.Set();
                }

                _socket.BeginReceive(_buffer, 0, _buffer.Length, 0,
                       new AsyncCallback(ReceiveCallback), _socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
               _socket.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), _socket);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        private void SendCallback(IAsyncResult AR)
        {
            try
            {

                int bytesSent = _socket.EndSend(AR);
                Console.WriteLine("Sent {0} bytes to Lightroom.", bytesSent);

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
