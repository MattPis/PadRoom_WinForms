using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using PadRoom.Network;

namespace PadRoom
{

    public class TrayContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        NetworkManager networkManager = new NetworkManager();

        public TrayContext()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    StatusMenuItem(),
                    RestartItem(),
                    IpAddressItem(),
                    ExitMenuItem()
                }),
                Visible = true
            };

            networkManager.StartService();
        }

        MenuItem StatusMenuItem()
        {
            return new MenuItem("Status", ShowConnectivityStatusForm);
        }
        
        MenuItem RestartItem()
        {
            return new MenuItem("Restart", Restart);
        }

        MenuItem IpAddressItem()
        {
            string address = "IP: " + NetworkManager.IpAddress().ToString();
            return new MenuItem(address, ShowIpAddress);
        }

        MenuItem ExitMenuItem()
        {
            return new MenuItem("Exit", Exit);
        }

        void ShowConnectivityStatusForm(object sender, EventArgs e)
        {
            ConnectivityStatusForm form = new ConnectivityStatusForm(networkManager.ConnectionStaus());
            form.Show();
        }

        void Restart(object sender, EventArgs e)
        {
            networkManager.RestartService();
        }

        void ShowIpAddress(object sender, EventArgs e)
        {
            //TODO: Reconsider this feature.
        }

        void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
