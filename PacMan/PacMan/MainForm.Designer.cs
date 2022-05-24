namespace PacMan
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Player = new PacMan.GameObjects.Player();
            this.Maze = new PacMan.GameObjects.Maze();
            this.MazeContainer = new System.Windows.Forms.Panel();
            this.MazeContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.ID = 0;
            this.Player.Location = new System.Drawing.Point(999, 300);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(106, 107);
            this.Player.TabIndex = 0;
            // 
            // Maze
            // 
            this.Maze.ID = 1;
            this.Maze.Location = new System.Drawing.Point(5, 5);
            this.Maze.Margin = new System.Windows.Forms.Padding(0);
            this.Maze.Name = "Maze";
            this.Maze.Size = new System.Drawing.Size(588, 671);
            this.Maze.TabIndex = 1;
            // 
            // MazeContainer
            // 
            this.MazeContainer.BackColor = System.Drawing.Color.Transparent;
            this.MazeContainer.Controls.Add(this.Maze);
            this.MazeContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.MazeContainer.Location = new System.Drawing.Point(0, 0);
            this.MazeContainer.Name = "MazeContainer";
            this.MazeContainer.Padding = new System.Windows.Forms.Padding(5);
            this.MazeContainer.Size = new System.Drawing.Size(640, 681);
            this.MazeContainer.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MazeContainer);
            this.Controls.Add(this.Player);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pac-Man";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MazeContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GameObjects.Player Player;
        private GameObjects.Maze Maze;
        private Panel MazeContainer;
    }
}