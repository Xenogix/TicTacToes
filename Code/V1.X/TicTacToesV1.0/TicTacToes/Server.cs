using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace TicTacToes
{
    class Server
    {
        private string clientIp;
        private NetworkStream stream;
        private TcpClient client;
        private int port;
        public bool listening = true;
        private byte[] bytes = new byte[256]; // Tableau qui contiendra les données
        private IPAddress localAddr; // Addresse ip du PC qui host
        public Game myGame;
        private TcpListener server;
        private Thread Listener;
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private bool disconnected = false;

        public void Listen()
        {
            Listener = new Thread(new ThreadStart(ListenThread));

            if (Listener.IsAlive)
                Listener.Abort();

            Listener.Start();
        }

        public void Close()
        {
            server.Stop();

            if (Listener != null && Listener.IsAlive)
                Listener.Abort();

            if(stream != null)
                stream.Close();

            if (client != null)
                client.Close();
        }

        public Server(string clientIp, int port, Game myGame, IPAddress localAddr)
        {
            this.myGame = myGame;
            this.clientIp = clientIp;
            this.port = port;
            this.localAddr = localAddr;

            t.Interval = 1000;
            t.Tick += new EventHandler(TestConnectedPlayer);
            t.Start();

            // On démarre la recherche de clients
            server = new TcpListener(localAddr, port);
            server.Start();
        }

        private void TestConnectedPlayer(object sender, EventArgs e)
        {
            
            if (!SendMessage("CONTEST"))
                Close();
        }

        public bool Connect(bool ready = false)
        {
            try
            {
                client = new TcpClient(clientIp, port);
                stream = client.GetStream();
                if (!ready) { SendMessage("CONNECT"); Listen(); }
                myGame.connected = true;
                return true;

            }
            catch (Exception e)
            {
                myGame.connected = false;
                return false;
            }
        }

        public bool SendMessage(string message)
        {
            try
            {
                stream.Write(GetBytes(message), 0, GetBytes(message).Length);
            }
            catch { return false; }

            return true;
        }

        private void ListenThread()
        {
            listening = true;

            try
            {
                // On prend le premier client qu'on trouve
                client = server.AcceptTcpClient();
            }
            catch { }

            // On écoute le client
            stream = client.GetStream();

            int i;

            // On lis les requêtes du client
            try
            {
                while (listening)
                {
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        TreatCommand(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    }
                    bytes = new Byte[256];
                }
            }
            catch (Exception e)
            {
                if (!disconnected)
                {
                    MessageBox.Show("Vous avez été déconnecté !");
                    disconnected = true;
                    myGame.Invoke(new Action(() => myGame.Close()));
                }
            }
        }

        private void TreatCommand(string str)
        {
            if (str.Substring(0, 7) == "CONNECT")
            {
                Connect(true);
                SendMessage("BEGIN");
                myGame.yourTurn = true;
                myGame.connected = true;
            }
            else if (str.Substring(0, 5) == "BEGIN")
            {
                myGame.yourTurn = false;
            }
            else if (str.Substring(0, 7) == "CONTEST" && myGame.connected == false)
            {
                listening = false;
                ListenThread();
            }
            else
            {
                try
                {
                    myGame.Invoke(new Action(() => myGame.OnlineTokenClick(Convert.ToInt32(str.Substring(0, 1)))));
                }
                catch (Exception e) { }
            }
        }

        /// <summary>
        /// Retourne un texte converti en byte
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private byte[] GetBytes(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
    }
}
