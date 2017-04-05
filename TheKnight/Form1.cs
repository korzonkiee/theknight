using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheKnight.Properties;

namespace TheKnight
{
    public partial class Form1 : Form
    {
        private int[,] boardColors = new int[8,8];

        private int knight_x = 0;
        private int knight_y = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "The Knight";
            this.Size = new Size(400, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.CenterToScreen();
            KeyDown += new KeyEventHandler(Form1_KeyPress);
            GenerateBoardColors(8);
            tableLayoutPanel1.CellPaint += TableLayoutPanel1_CellPaint;
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
                switch (e.KeyValue)
                {
                    case (char)Keys.Up:
                    knight_y--;
                    break;
                    case (char)Keys.Right:
                    knight_x++;
                    break;
                    case (char)Keys.Down:
                    knight_y++;
                    break;
                    case (char)Keys.Left:
                    knight_x--;
                    break;
                }

            tableLayoutPanel1.Invalidate();
        }

        private void GenerateBoardColors(int x)
        {
            var random = new Random();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    var value = random.Next(0, 2);
                    boardColors[i, j] = value;
                }
            }
        }

        private void TableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (boardColors[e.Column, e.Row] == 0)
            {
                e.Graphics.FillRectangle(Brushes.ForestGreen, e.CellBounds);
            }
            else if (boardColors[e.Column, e.Row] == 1)
            {
                e.Graphics.FillRectangle(Brushes.Maroon, e.CellBounds);
            }

            if (e.Column == knight_x && e.Row == knight_y)
            {
                e.Graphics.DrawImageUnscaledAndClipped(Resources.knight_left, e.CellBounds);
            }

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateBoardColors(8);
            tableLayoutPanel1.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            DialogResult result1 = MessageBox.Show("Exit?", "Question",
                MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            } else
            {
                Enabled = true;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new Settings(this);
            settings.Show();
        }

        public void RecreateBoard(int newBoardSize)
        {
            tableLayoutPanel1.CellPaint -= TableLayoutPanel1_CellPaint;
            tableLayoutPanel1.RowCount = newBoardSize;
            tableLayoutPanel1.ColumnCount = newBoardSize;
            tableLayoutPanel1.CellPaint += TableLayoutPanel1_CellPaint;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            for (int i = 0; i < newBoardSize; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / newBoardSize));
                for (int j = 0; j < newBoardSize; j++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / newBoardSize));
                }
            }

            boardColors = new int[newBoardSize, newBoardSize];
            GenerateBoardColors(newBoardSize);

            tableLayoutPanel1.Invalidate();
        }
    }
}
