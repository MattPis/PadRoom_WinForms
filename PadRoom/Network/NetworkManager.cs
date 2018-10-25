using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;


namespace PadRoom.Network
{

    public struct ConnectionStatusDto
    {
        public readonly bool networkClient, lrSender, lrReciever;

        public ConnectionStatusDto(bool client, bool sender, bool reciever)
        {
            networkClient = client;
            lrSender = sender;
            lrReciever = reciever;
        }
    }
    /*
    This class is dedicated to managing connections and data transmits between Network server and lightroom clients.
    Since Lightrom opens two independent sockets at different ports, equal number of clients is needed to
    maintain two way communication.
    */

    class NetworkManager
    {
        private static readonly int LRSendPort = 58763;
        private static readonly int LRRecievePort = 58764;

        private NetworkServer _networkServer = new NetworkServer();
        private LightroomClient _lightroomReciever = new LightroomClient();
        private LightroomClient _lightroomSender = new LightroomClient();

        public void StartService()
        {
            var serverThread = new Thread(new ThreadStart(_networkServer.StartServer));
            serverThread.Start();
            NetworkServer.MessageRecievedHandler += OnNetworkMessage;
            
            Thread LRSendThread = new Thread(new ParameterizedThreadStart(_lightroomSender.Start));
            LRSendThread.Start(LRSendPort);
            
            
            Thread LRRecieveThread = new Thread(new ParameterizedThreadStart(_lightroomReciever.Start));
            LRRecieveThread.Start(LRRecievePort);
            _lightroomReciever.MessageRecievedHandler += OnLightroomMesage;
        }

        public void StopService()
        {
            _networkServer.StopServer();
            _lightroomSender.Stop();
            _lightroomReciever.Stop();
        }

        public void RestartService()
        {
            StopService();
            StartService();
        }

        public ConnectionStatusDto ConnectionStaus()
        {
            return new ConnectionStatusDto(
                client: _networkServer.IsConnected(),
                sender: _lightroomSender.IsConnected(),
                reciever: _lightroomSender.IsConnected());
        }

        private void OnNetworkMessage(object sender, EventArgs e)
        {
            byte[] data = (byte[])sender;
            _lightroomSender.Send(data);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine("Network Server did Recieve message: " + text);
        }

        private void OnLightroomMesage(object sender, EventArgs e)
        {
            byte[] data = (byte[])sender;
            _networkServer.Send(data);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine("Lightroom client did Recieve message: " + text);
        }

        public static IPAddress IpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP Address Not Found!");

        }
    }

}

