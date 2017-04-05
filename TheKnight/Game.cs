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
    public partial class Game : Form
    {

        private Scenery scenery;
        private Knight knight;

        public Game()
        {
            InitializeComponent();
            this.Text = "The Knight";
            this.Size = new Size(400, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.CenterToScreen();

            KeyDown += new KeyEventHandler(KnightWalkHandler);
            gameBoardLayout.CellPaint += ColorBoard;

            scenery = new Scenery();
            knight = new Knight();
            DrawKnight();
        }

        private void KnightWalkHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != (char)Keys.Up && e.KeyValue != (char)Keys.Right && e.KeyValue != (char)Keys.Left && e.KeyValue != (char)Keys.Down)
                return;

            Walk walk = Walk.None;

            switch (e.KeyValue)
            {
            case (char)Keys.Up:
                walk = Walk.Up;
                break;

            case (char)Keys.Right:
                walk = Walk.Right;
                break;

            case (char)Keys.Down:
                walk = Walk.Down;
                break;

            case (char)Keys.Left:
                walk = Walk.Left;
                break;

            default:
                break;
            }

            var potentialPosition = Position.Create(knight.Position, walk);

            if (IsPositionValid(potentialPosition))
            {
                RemoveKnight();
                knight.SetPosition(potentialPosition, walk);
                DrawKnight();
            }

        }

        private void ColorBoard(object sender, TableLayoutCellPaintEventArgs e)
        {
            var control = gameBoardLayout.GetControlFromPosition(e.Column, e.Row);

            if (control != null)
            {
                var graphics = e.Graphics;
                
                if (scenery.Board[e.Column, e.Row] == SceneryElement.Grass)
                {
                    graphics.FillRectangle(Brushes.ForestGreen, e.CellBounds);
                    control.BackColor = Color.ForestGreen;
                }
                else if (scenery.Board[e.Column, e.Row] == SceneryElement.Wall)
                {
                    graphics.FillRectangle(Brushes.Maroon, e.CellBounds);
                    control.BackColor = Color.Maroon;
                }

            }

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scenery.GenerateRandomColors();
            gameBoardLayout.Invalidate();
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

        public void RecreateBoard(short newBoardSize)
        {
            gameBoardLayout.CellPaint -= ColorBoard;
            gameBoardLayout.RowCount = newBoardSize;
            gameBoardLayout.ColumnCount = newBoardSize;
            gameBoardLayout.CellPaint += ColorBoard;
            gameBoardLayout.RowStyles.Clear();
            gameBoardLayout.ColumnStyles.Clear();
            gameBoardLayout.Controls.Clear();

            for (int i = 0; i < newBoardSize; i++)
            {
                gameBoardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / newBoardSize));
                gameBoardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / newBoardSize));
            }

            for (int i = 0; i < newBoardSize; i++)
            {
                for (int j = 0; j < newBoardSize; j++)
                {
                    gameBoardLayout.Controls.Add(new Panel() { Dock = DockStyle.Fill }, i, j);
                }
            }

            scenery.SetBoardSize(newBoardSize);
            gameBoardLayout.Invalidate();
        }
        
        private void DrawKnight()
        {
            var cellControl = gameBoardLayout.GetControlFromPosition(knight.Position.X, knight.Position.Y);
            var knightImage = knight.Image;

            knightImage.MakeTransparent();
            cellControl.Anchor = AnchorStyles.None;
            cellControl.BackgroundImage = knightImage;
            cellControl.BackgroundImageLayout = ImageLayout.Stretch | ImageLayout.Center;
        }

        private void RemoveKnight()
        {
            var cellControl = gameBoardLayout.GetControlFromPosition(knight.Position.X, knight.Position.Y);
            var cellGraphics = cellControl.CreateGraphics();

            cellControl.BackgroundImage = null;
        }

        private bool IsPositionValid(Position position)
        {
            // Out of board bounds
            if (position.X < 0 || position.Y < 0 ||
                position.X >= scenery.BoardSize || position.Y >= scenery.BoardSize)
            {
                return false;
            }

            // Stepped at not available position
            if (scenery.NotAvailablePosition(position))
            {
                return false;
            }

            return true;
        }
    }
}
