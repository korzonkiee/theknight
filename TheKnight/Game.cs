using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheKnight.Properties;

namespace TheKnight
{
    public partial class Game : Form
    {
        private Scenery scenery;
        private Knight knight;
        private SplashScreen splashScreen;

        private bool EditMode = false;
        private List<PictureBox> PictureBoxes;
        private PictureBox selectedPictureBox;

        public Game()
        {
            HandleSplashScreen();

            //nie wiem jak inaczej xd
            TopMost = true;
            InitializeComponent();
            ResumeLayout(true);

            this.Text = "The Knight";
            this.Size = new Size(400, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.CenterToScreen();

            PictureBoxes = new List<PictureBox>();
            scenery = new Scenery();
            var knightSpawn = scenery.GetKnightSpawn();
            knight = new Knight(knightSpawn);
            RecreateBoard(scenery.BoardSize);

            DrawKnight();
            DrawKey();
            DrawDoor(false);

            KeyDown += new KeyEventHandler(KnightWalkHandler);
        }

        private void HandleSplashScreen()
        {
            splashScreen = new SplashScreen();
            Thread thread = new Thread(new ThreadStart(SplashStart));
            thread.Start();

            Thread.Sleep(3000);

            thread.Abort();
        }

        private void SplashStart()
        {
            Application.Run(splashScreen);
        }

        private void KnightWalkHandler(object sender, KeyEventArgs e)
        {

            if (e.KeyValue != (char)Keys.Space && e.KeyValue != (char)Keys.Up && e.KeyValue != (char)Keys.Right && e.KeyValue != (char)Keys.Left && e.KeyValue != (char)Keys.Down)
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

            case (char)Keys.Space:
                DestroyWalls();
                break;

            default:
                break;
            }

            var potentialPosition = Position.Create(knight.Position, walk);

            if (IsPositionValid(potentialPosition))
            {
                if (IsKeySpawn(potentialPosition))
                {
                    knight.HasKey = true;
                    DrawDoor(); //Draw closed doors;
                }
                if (IsOpenedDoorSpawn(potentialPosition))
                {
                    knight.HasKey = false;
                    RemoveDoor();
                    RemoveKnight();
                    scenery.GenerateRandomColors();
                    knight = new Knight(scenery.GetKnightSpawn());
                    DrawKnight();
                    DrawKey();
                    DrawDoor(false);
                    ColorBoard();
                }
                else
                {
                    RemoveKnight();
                    knight.SetPosition(potentialPosition, walk);
                    DrawKnight();
                }
            }

        }

        private void ColorBoard()
        {
            for (int i = 0; i < scenery.BoardSize; i++)
            {
                for (int j = 0; j < scenery.BoardSize; j++)
                {
                    var control = gameBoardLayout.GetControlFromPosition(i, j);

                    if (control != null)
                    {
                        if (scenery.Board[i, j] == SceneryElement.Grass)
                        {
                            (control as PictureBox).BackColor = Color.ForestGreen;
                        }
                        else if (scenery.Board[i, j] == SceneryElement.Wall)
                        {
                            (control as PictureBox).BackColor = Color.Maroon;
                        }
                    }
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveKey();
            RemoveDoor();

            scenery.GenerateRandomColors();
            ColorBoard();

            DrawKey();
            DrawDoor(false);

            var knightSpawn = scenery.GetKnightSpawn();

            RemoveKnight();
            knight.SetPosition(knightSpawn);
            DrawKnight();
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
            gameBoardLayout.Visible = false;
            gameBoardLayout.RowCount = newBoardSize;
            gameBoardLayout.ColumnCount = newBoardSize;
            gameBoardLayout.Controls.Clear();
            gameBoardLayout.RowStyles.Clear();
            gameBoardLayout.ColumnStyles.Clear();
            PictureBoxes.Clear();

            for (int i = 0; i < newBoardSize; i++)
            {
                gameBoardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / newBoardSize));
                gameBoardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / newBoardSize));
            }

            for (short i = 0; i < newBoardSize; i++)
            {
                for (short j = 0; j < newBoardSize; j++)
                {
                    var pictureBox = GeneratePictureBox();
                    pictureBox.Tag = new Position(j, i);
                    gameBoardLayout.Controls.Add(pictureBox);
                    PictureBoxes.Add(pictureBox);
                }
            }

            scenery.SetBoardSize(newBoardSize);
            ColorBoard();
            DrawKey();
            DrawDoor(false);

            var knightSpawn = scenery.GetKnightSpawn();
            RemoveKnight();
            knight.SetPosition(knightSpawn);
            DrawKnight();
            
            gameBoardLayout.Visible = true;
        }

        private PictureBox GeneratePictureBox()
        {
            return new PictureBox()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0)
            };

        }
        
        private void DrawKnight()
        {
            var cellControl = gameBoardLayout.GetControlFromPosition(knight.Position.X, knight.Position.Y);
            var knightImage = knight.Image;

            knightImage.MakeTransparent();
            (cellControl as PictureBox).Image = knightImage;
            (cellControl as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void RemoveKnight()
        {
            var cellControl = gameBoardLayout.GetControlFromPosition(knight.Position.X, knight.Position.Y);
            (cellControl as PictureBox).Image = null;
        }

        private void DrawKey()
        {
            var keyPosition = scenery.GetKeySpawn();

            var cellControl = gameBoardLayout.GetControlFromPosition(keyPosition.X, keyPosition.Y);
            var keyImage = Resources.key2;
            keyImage.MakeTransparent();
            (cellControl as PictureBox).Image = keyImage;
            (cellControl as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void RemoveKey()
        {
            var keyPosition = scenery.GetKeySpawn();
            var cellControl = gameBoardLayout.GetControlFromPosition(keyPosition.X, keyPosition.Y);
            (cellControl as PictureBox).Image = null;
        }

        private void DrawDoor(bool opened = true)
        {
            var doorPosition = scenery.GetDoorSpawn();
            var cellControl = gameBoardLayout.GetControlFromPosition(doorPosition.X, doorPosition.Y);
            var doorImage = opened ? Resources.opened_door : Resources.closed_door;

            doorImage.MakeTransparent();
            (cellControl as PictureBox).Image = doorImage;
            (cellControl as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void RemoveDoor()
        {
            var doorPosition = scenery.GetDoorSpawn();
            var cellControl = gameBoardLayout.GetControlFromPosition(doorPosition.X, doorPosition.Y);
            (cellControl as PictureBox).Image = null;
        }

        private void DestroyWalls()
        {
            var cellsToDestroy = new List<Tuple<SceneryElement, Position>>();

            cellsToDestroy.Add(GetCellType(knight.Position.X, (short)(knight.Position.Y - 1)));
            cellsToDestroy.Add(GetCellType((short)(knight.Position.X + 1), knight.Position.Y));
            cellsToDestroy.Add(GetCellType(knight.Position.X, (short)(knight.Position.Y + 1)));
            cellsToDestroy.Add(GetCellType((short)(knight.Position.X - 1), knight.Position.Y));

            foreach (var cellToDestroy in cellsToDestroy)
            {
                if (cellToDestroy != null)
                {
                    if (cellToDestroy.Item2 == scenery.GetKeySpawn() || 
                        cellToDestroy.Item2 == scenery.GetDoorSpawn())
                        continue;

                    if (cellToDestroy.Item1 == SceneryElement.Grass)
                        continue;

                    RemoveWall(cellToDestroy.Item2);
                }
            }
        }
        
        private Tuple<SceneryElement, Position> GetCellType(short x, short y)
        {
            if (x < 0 || y < 0 || x >= scenery.BoardSize || y >= scenery.BoardSize)
                return null;

            return Tuple.Create<SceneryElement, Position>(
                scenery.Board[x, y],
                new Position(x, y)
            );
        }        

        private void RemoveWall(Position position)
        {
            scenery.Board[position.X, position.Y] = SceneryElement.Grass;
            var cellControl = gameBoardLayout.GetControlFromPosition(position.X, position.Y);            
            cellControl.BackColor = Color.ForestGreen;
            cellControl.Invalidate();
        }


        private bool IsPositionValid(Position position)
        {
            // Out of board bounds
            if (position.X < 0 || position.Y < 0 ||
                position.X >= scenery.BoardSize || position.Y >= scenery.BoardSize)
                return false;

            // Stepped at not available position
            if (scenery.NotAvailablePosition(position))
                return false;

            if (position == scenery.GetDoorSpawn() && !knight.HasKey)
                return false;

            return true;
        }

        private bool IsKeySpawn(Position position)
        {
            var keySpawn = scenery.GetKeySpawn();

            if (position == keySpawn)
                return true;
            return false;
        }

        private bool IsOpenedDoorSpawn(Position position)
        {
            var doorSpawn = scenery.GetDoorSpawn();

            if (position == doorSpawn && knight.HasKey)
                return true;
            return false;
        }

        private void editModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                foreach (var pictureBox in PictureBoxes)
                {
                    pictureBox.Click += pictureBox_Click;
                }
                EditMode = true;
                var menuItem = (ToolStripItem)sender;
                menuItem.Text = "&Game mode";
                leftClickToolStripMenuItem.Visible = true;
                menuStrip1.BackColor = Color.LightYellow;
            }
            else
            {
                foreach (var pictureBox in PictureBoxes)
                {
                    pictureBox.Click -= pictureBox_Click;
                }

                EditMode = false;
                var menuItem = (ToolStripItem)sender;
                menuItem.Text = "&Edit mode";
                leftClickToolStripMenuItem.Visible = false;
                menuStrip1.BackColor = Control.DefaultBackColor;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            selectedPictureBox = sender as PictureBox;
            if (mouse.Button == MouseButtons.Left
                && (Position)selectedPictureBox.Tag != scenery.GetKeySpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetKnightSpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetDoorSpawn())
            {
                var pos = (Position)selectedPictureBox.Tag;
                if (grassToolStripMenuItem.Checked)
                {
                    selectedPictureBox.BackColor = Color.ForestGreen;
                    scenery.Board[pos.X, pos.Y] = SceneryElement.Grass;
                }
                else if (wallToolStripMenuItem.Checked)
                {
                    selectedPictureBox.BackColor = Color.Maroon;
                    scenery.Board[pos.X, pos.Y] = SceneryElement.Wall;
                }
            }
            if (mouse.Button == MouseButtons.Right)
            {
                var contextMenuStrip = new ContextMenuStrip();
                ToolStripItem door = contextMenuStrip.Items.Add("Door");
                door.Click += new EventHandler(door_Click);
                ToolStripItem knight = contextMenuStrip.Items.Add("Knight");
                knight.Click += new EventHandler(knight_Click);
                ToolStripItem key = contextMenuStrip.Items.Add("Key");
                key.Click += new EventHandler(key_Click);
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        void door_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if ((Position)selectedPictureBox.Tag != scenery.GetKnightSpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetKeySpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetDoorSpawn())
            {
                PictureBox previousBox = (PictureBox)gameBoardLayout.GetControlFromPosition(scenery.GetDoorSpawn().X, scenery.GetDoorSpawn().Y);
                previousBox.Image = null;
                selectedPictureBox.BackColor = Color.ForestGreen;
                var image = (knight.HasKey ? Resources.opened_door : Resources.closed_door);
                image.MakeTransparent();
                selectedPictureBox.Image = image;
                selectedPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                scenery.SetDoorSpawn((Position)selectedPictureBox.Tag);
            }
        }

        void knight_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if ((Position)selectedPictureBox.Tag != scenery.GetKnightSpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetKeySpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetDoorSpawn())
            {
                PictureBox previousBox = (PictureBox)gameBoardLayout.GetControlFromPosition(knight.Position.X, knight.Position.Y);
                previousBox.Image = null;
                selectedPictureBox.BackColor = Color.ForestGreen;
                var image = (knight.lastWalk == Walk.Left ? Resources.knight_left : Resources.knight_right);
                image.MakeTransparent();
                selectedPictureBox.Image = image;
                selectedPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                knight.SetPosition((Position)selectedPictureBox.Tag, knight.lastWalk);
            }
        }

        void key_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if ((Position)selectedPictureBox.Tag != scenery.GetKnightSpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetKeySpawn()
                && (Position)selectedPictureBox.Tag != scenery.GetDoorSpawn())
            {
                PictureBox previousBox = (PictureBox)gameBoardLayout.GetControlFromPosition(scenery.GetKeySpawn().X, scenery.GetKeySpawn().Y);
                previousBox.Image = null;
                selectedPictureBox.BackColor = Color.ForestGreen;
                var image = Resources.key2;
                image.MakeTransparent();
                selectedPictureBox.Image = image;
                selectedPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                scenery.SetKeySpawn((Position)selectedPictureBox.Tag);
            }
        }

        private void wallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grassToolStripMenuItem.Checked = false;
            wallToolStripMenuItem.Checked = true;
        }

        private void grassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grassToolStripMenuItem.Checked = true;
            wallToolStripMenuItem.Checked = false;
        }
    }
}
