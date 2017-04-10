namespace TheKnight
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameBoardLayout = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editModeToolStripMenuItem,
            this.leftClickToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(426, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(246, 30);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(246, 30);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(246, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editModeToolStripMenuItem
            // 
            this.editModeToolStripMenuItem.Name = "editModeToolStripMenuItem";
            this.editModeToolStripMenuItem.Size = new System.Drawing.Size(106, 29);
            this.editModeToolStripMenuItem.Text = "Edit mode";
            this.editModeToolStripMenuItem.Click += new System.EventHandler(this.editModeToolStripMenuItem_Click);
            // 
            // leftClickToolStripMenuItem
            // 
            this.leftClickToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wallToolStripMenuItem,
            this.grassToolStripMenuItem});
            this.leftClickToolStripMenuItem.Name = "leftClickToolStripMenuItem";
            this.leftClickToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.leftClickToolStripMenuItem.Text = "Left click";
            this.leftClickToolStripMenuItem.Visible = false;
            // 
            // wallToolStripMenuItem
            // 
            this.wallToolStripMenuItem.Checked = true;
            this.wallToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wallToolStripMenuItem.Name = "wallToolStripMenuItem";
            this.wallToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.wallToolStripMenuItem.Text = "Wall";
            this.wallToolStripMenuItem.Click += new System.EventHandler(this.wallToolStripMenuItem_Click);
            // 
            // grassToolStripMenuItem
            // 
            this.grassToolStripMenuItem.Name = "grassToolStripMenuItem";
            this.grassToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.grassToolStripMenuItem.Text = "Grass";
            this.grassToolStripMenuItem.Click += new System.EventHandler(this.grassToolStripMenuItem_Click);
            // 
            // gameBoardLayout
            // 
            this.gameBoardLayout.ColumnCount = 8;
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameBoardLayout.Location = new System.Drawing.Point(0, 35);
            this.gameBoardLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gameBoardLayout.Name = "gameBoardLayout";
            this.gameBoardLayout.RowCount = 8;
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardLayout.Size = new System.Drawing.Size(426, 368);
            this.gameBoardLayout.TabIndex = 1;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 403);
            this.Controls.Add(this.gameBoardLayout);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Game";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel gameBoardLayout;
        private System.Windows.Forms.ToolStripMenuItem editModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grassToolStripMenuItem;
    }
}

