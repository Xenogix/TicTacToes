using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TicTacToes
{
    public class Token
    {
        public Bitmap image = Properties.Resources.voidSpot;

        public Token(Bitmap image)
        {
            this.image = image;
        }
    }
}
