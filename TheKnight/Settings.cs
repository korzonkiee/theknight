using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheKnight
{
    public partial class Settings : Form
    {
        private readonly Game game;
        public Settings(Game game)
        {
            this.game = game;
            InitializeComponent();
            ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            short newBoardSize = 0;
            if (checkBox1.Checked)
                newBoardSize = 8;
            else if (checkBox2.Checked)
                newBoardSize = 10;
            else if (checkBox3.Checked)
                newBoardSize = 12;

            if (newBoardSize != 0)
            {
                game.RecreateBoard(newBoardSize);
                Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
