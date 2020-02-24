using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToes
{
    public partial class Game : Form
    {
        private int width;
        private int height;
        private int size = 100;

        private const int marginBottom = 35;
        private const int marginRight = 15;
        private const int TOKENMARGINTOP = 0;

        private PictureBox lastSelection;

        private PictureBox[,] tokens;
        private PictureBox[,] backgroundTokens;

        PictureBox[] tempToken = new PictureBox[2];

        private Board board;

        private Player[] players;

        private bool turn = true;
        public bool yourTurn = false;
        public bool isWaitingClosed = false;
        private Server server;

        public bool connected = false;

        private Waiting waiting;

        public void Wait(object o)
        {
            waiting.ShowDialog();
        }

        public Game(bool role, string clientIp, string myIP, int port, int width = 7, int height = 6)
        {
            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);

            waiting = new Waiting(this);
            ThreadPool.QueueUserWorkItem(Wait);

            this.width = width;
            this.height = height;
            tokens = new PictureBox[width, height];
            backgroundTokens = new PictureBox[width, height];
            board = new Board(height, width);

            Height = height * size;
            Width = width * size + marginRight;

            CreateTokens();

            Height = height * size + marginBottom;

            LoadTokenImage();

            server = new Server(clientIp, port, this, IPAddress.Parse(myIP));

            Start();

            if (role)
            {
                server.Listen();

                while (!connected) {
                    if (isWaitingClosed)
                    {
                        this.Close();
                        connected = true;
                    }
                }
            }
            else
            {
                while (!connected)
                {
                    connected = server.Connect();

                    if (isWaitingClosed)
                    {
                        this.Close();
                        connected = true;
                    }
                }
            }

            if (isWaitingClosed != true)
            {
                waiting.Invoke(new Action(() => waiting.Close()));

                if (yourTurn)
                    MessageBox.Show("Vous commencez la partie");
                else
                    MessageBox.Show("L'adversaire commence la partie");
            }
            else
            {
                server.Close();
            }
        }

        private void CreateTokens()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    backgroundTokens[x, y] = new PictureBox();
                    backgroundTokens[x, y].Size = new Size(size, size);
                    backgroundTokens[x, y].Top = Height - (size * (y + 1));
                    backgroundTokens[x, y].Left = 0 + ((size - 1) * x);
                    backgroundTokens[x, y].SizeMode = PictureBoxSizeMode.Zoom;
                    backgroundTokens[x, y].BackColor = Color.Gray;
                    Controls.Add(backgroundTokens[x, y]);
                }

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    tokens[x, y] = new PictureBox();
                    tokens[x, y].Size = new Size(size, size);
                    tokens[x, y].SizeMode = PictureBoxSizeMode.Zoom;
                    tokens[x, y].Top = 0;
                    tokens[x, y].MouseMove += new MouseEventHandler(SelectToken);
                    tokens[x, y].Left = 0;
                    tokens[x, y].BackColor = Color.Transparent;
                    backgroundTokens[x, y].Controls.Add(tokens[x, y]);
                    tokens[x, y].BringToFront();
                }

            for (int i = 0; i < tempToken.Count(); i++)
            {
                tempToken[i] = new PictureBox();
                tempToken[i].Size = new Size(size, size);
                tempToken[i].SizeMode = PictureBoxSizeMode.Zoom;
                tempToken[i].Top = 0;
                tempToken[i].Left = 0;
                tempToken[i].Image = Properties.Resources.voidSpot;
                tempToken[i].BackColor = Color.Transparent;
            }
        }

        private void Start()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    tokens[x, y].Click += new EventHandler(TokenClick);
                }
        }

        public void OnlineTokenClick(int x)
        {
            if (turn)
            {
                board.AddToken(x, new Token(Properties.Resources.redDot));
                AnimateToken(Properties.Resources.redDot, x);
            }
            else
            {
                board.AddToken(x, new Token(Properties.Resources.yellowDot));
                AnimateToken(Properties.Resources.yellowDot, x);
            }

            UpdateToken(x, board.playerTokens[x].Count() - 1);

            turn = !turn;
        }

        private bool testWin(int x, int y)
        {

            int count = 0;


            int xt = x;
            int yt = y;

            while(xt > 0 && xt < width)
            {
                if (board.playerTokens[x][y].image == board.playerTokens[xt][y].image)
                    xt -= 1;
            }

            for (yt = y; yt > width; yt--)
                if (board.playerTokens[x][y].image == board.playerTokens[yt][y].image)
                {
                    count++;

                    if (count == 4)
                        return true;
                }

            yt = y;
            xt = x;



            return false;
        }

        private void Win()
        {

        }

        private void TokenClick(object sender, EventArgs e)
        {
            if (yourTurn == turn)
            {
                Point mousePosition = PointToClient(new Point(MousePosition.X, MousePosition.Y));

                if (server.SendMessage((mousePosition.X / size).ToString()))
                {
                    if (turn)
                    {
                        board.AddToken(mousePosition.X / size, new Token(Properties.Resources.redDot));
                        AnimateToken(Properties.Resources.redDot, mousePosition.X / size);
                    }
                    else
                    {
                        board.AddToken(mousePosition.X / size, new Token(Properties.Resources.yellowDot));
                        AnimateToken(Properties.Resources.yellowDot, mousePosition.X / size);
                    }

                    UpdateToken(mousePosition.X / size, board.playerTokens[mousePosition.X / size].Count() - 1);

                    turn = !turn;
                }
            }
        }

        private void LoadTokenImage()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    tokens[x, y].Image = board.boardTokens[x, y].image;
                }
        }

        private void UpdateToken(int x, int y)
        {
            backgroundTokens[x, y].Image = board.playerTokens[x][board.playerTokens[x].Count() - 1].image;
        }

        private void MoveToken(int dy, PictureBox p)
        {
            p.Height = p.Height + dy;
            Update();
        }

        private void AnimateToken(Bitmap image, int x)
        {
            for (int y = height - 1; y > board.playerTokens[x].Count() - 1; y--)
            {
                backgroundTokens[x, y].Image = image;


                if (y > 0)
                {
                    backgroundTokens[x, y - 1].Top -= size;
                    backgroundTokens[x, y - 1].Height += size;
                    backgroundTokens[x, y].BringToFront();

                    backgroundTokens[x, y].Controls.Add(tempToken[0]);
                    tempToken[0].Top = size;

                    backgroundTokens[x, y - 1].Controls.Add(tempToken[1]);
                    tempToken[1].Top = size;

                    if (y > 1)
                    {
                        backgroundTokens[x, y - 2].BringToFront();
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    MoveToken(20, backgroundTokens[x, y]);

                    Thread.Sleep(4);
                }

                if (y != 0)
                {
                    backgroundTokens[x, y - 1].Top += size;
                    backgroundTokens[x, y - 1].Height = size;
                    tokens[x, y - 1].Top = 0;
                }

                backgroundTokens[x, y].Height = size;
                backgroundTokens[x, y].Image = null;
            }
        }

        private void SelectToken(object sender, EventArgs e)
        {
            Point mousePosition = PointToClient(new Point(MousePosition.X, MousePosition.Y));

            try
            {

                PictureBox selection = tokens[mousePosition.X / size, (Height - mousePosition.Y - size / 2 + TOKENMARGINTOP) / size];

                if (selection != lastSelection)
                {
                    selection.BorderStyle = BorderStyle.Fixed3D;

                    if (lastSelection == null)
                        lastSelection = selection;
                    else
                    {
                        lastSelection.BorderStyle = BorderStyle.None;

                        lastSelection = selection;
                    }
                }
            }
            catch (Exception) { }
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.Close();
        }
    }
}
