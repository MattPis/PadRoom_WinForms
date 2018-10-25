using PadRoom.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PadRoom
{
    public partial class ConnectivityStatusForm : Form
    {
        public ConnectivityStatusForm(ConnectionStatusDto status)
        {
            InitializeComponent();
            iPadStatusLabel.Text = status.networkClient == true ? "Connected" : "Connecting...";
            iPadStatusLabel.ForeColor = status.networkClient == true ? Color.Green : Color.White;
            lrStatusLabel.Text = status.lrReciever == true && status.lrSender == true ? "Connected" : "Connecting...";
            lrStatusLabel.ForeColor = status.lrReciever == true && status.lrSender == true ? Color.Green : Color.White;
        }
    }
}
