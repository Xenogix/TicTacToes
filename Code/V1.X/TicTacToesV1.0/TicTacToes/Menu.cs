using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToes
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

            connectionPanel.txtLocalIP.Text = GetLocalIPAddress();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            connectionPanel.btnConnection.Click += new EventHandler(btnConnection_Click);
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            Hide();

            try
            {
               Game game = new Game(TryConnection(), connectionPanel.txtHostIP.Text, connectionPanel.txtLocalIP.Text, Convert.ToInt32(connectionPanel.txtHostPort.Text));

               if (!game.IsDisposed)
                game.ShowDialog();
            }
            catch(Exception ew)
            {
                MessageBox.Show("Une Erreur est survenue.\nVeuillez entrer des informations correctes");
            }

            Show();
        }

        private bool TryConnection()
        {
            try
            {
                var client = new TcpClient();
                var result = client.BeginConnect(connectionPanel.txtHostIP.Text, Convert.ToInt32(connectionPanel.txtHostPort.Text), null, null);

                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                NetworkStream stream = client.GetStream();
                stream.Write(GetBytes("CONTEST"),0, GetBytes("CONTEST").Length);
            }
            catch
            {
                return false;
            }

            return false;
        }

        private byte[] GetBytes(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "";
        }
    }
}
