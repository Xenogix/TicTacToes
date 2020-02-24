using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToes
{
    public partial class Waiting : Form
    {
        Game ended;

        public Waiting(Game ended)
        {
            this.ended = ended;
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);

            InitializeComponent();
        }

        private void TestValue()
        {
            if (ended.connected)
                Close();
        }

        private void Waiting_Load(object sender, EventArgs e)
        {
            label1.BringToFront();
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ended.isWaitingClosed = true;
        }
    }
}
