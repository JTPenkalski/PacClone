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
            this.label1 = new System.Windows.Forms.Label();
            this.MazeContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.ID = 0;
            this.Player.Location = new System.Drawing.Point(424, 728);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(48, 48);
            this.Player.TabIndex = 0;
            // 
            // Maze
            // 
            this.Maze.ID = 1;
            this.Maze.Location = new System.Drawing.Point(0, 0);
            this.Maze.Margin = new System.Windows.Forms.Padding(0);
            this.Maze.Name = "Maze";
            this.Maze.Size = new System.Drawing.Size(896, 992);
            this.Maze.TabIndex = 1;
            // 
            // MazeContainer
            // 
            this.MazeContainer.BackColor = System.Drawing.Color.Transparent;
            this.MazeContainer.Controls.Add(this.Player);
            this.MazeContainer.Controls.Add(this.Maze);
            this.MazeContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.MazeContainer.Location = new System.Drawing.Point(5, 5);
            this.MazeContainer.Margin = new System.Windows.Forms.Padding(0);
            this.MazeContainer.Name = "MazeContainer";
            this.MazeContainer.Size = new System.Drawing.Size(944, 1001);
            this.MazeContainer.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(952, 659);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1484, 1011);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MazeContainer);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
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
        private Label label1;
    }
}