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
            this.LivesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.LifeIcon1 = new System.Windows.Forms.PictureBox();
            this.LifeIcon2 = new System.Windows.Forms.PictureBox();
            this.RedGhost = new PacMan.GameObjects.Ghost();
            this.MazeContainer.SuspendLayout();
            this.LivesContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LifeIcon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LifeIcon2)).BeginInit();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.Transparent;
            this.Player.ID = 0;
            this.Player.Location = new System.Drawing.Point(424, 729);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(47, 47);
            this.Player.TabIndex = 0;
            // 
            // Maze
            // 
            this.Maze.BackColor = System.Drawing.Color.Transparent;
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
            this.MazeContainer.Controls.Add(this.RedGhost);
            this.MazeContainer.Controls.Add(this.label1);
            this.MazeContainer.Controls.Add(this.LivesContainer);
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
            this.label1.Location = new System.Drawing.Point(921, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 2;
            // 
            // LivesContainer
            // 
            this.LivesContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LivesContainer.Controls.Add(this.LifeIcon1);
            this.LivesContainer.Controls.Add(this.LifeIcon2);
            this.LivesContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LivesContainer.Location = new System.Drawing.Point(904, 8);
            this.LivesContainer.Margin = new System.Windows.Forms.Padding(8);
            this.LivesContainer.Name = "LivesContainer";
            this.LivesContainer.Size = new System.Drawing.Size(32, 320);
            this.LivesContainer.TabIndex = 3;
            // 
            // LifeIcon1
            // 
            this.LifeIcon1.Image = global::PacMan.Properties.Resources.Pac01;
            this.LifeIcon1.Location = new System.Drawing.Point(0, 0);
            this.LifeIcon1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.LifeIcon1.Name = "LifeIcon1";
            this.LifeIcon1.Size = new System.Drawing.Size(32, 32);
            this.LifeIcon1.TabIndex = 0;
            this.LifeIcon1.TabStop = false;
            // 
            // LifeIcon2
            // 
            this.LifeIcon2.Image = global::PacMan.Properties.Resources.Pac01;
            this.LifeIcon2.Location = new System.Drawing.Point(0, 37);
            this.LifeIcon2.Margin = new System.Windows.Forms.Padding(0);
            this.LifeIcon2.Name = "LifeIcon2";
            this.LifeIcon2.Size = new System.Drawing.Size(32, 32);
            this.LifeIcon2.TabIndex = 1;
            this.LifeIcon2.TabStop = false;
            // 
            // RedGhost
            // 
            this.RedGhost.BackColor = System.Drawing.Color.Transparent;
            this.RedGhost.ID = 2;
            this.RedGhost.Location = new System.Drawing.Point(424, 344);
            this.RedGhost.Name = "RedGhost";
            this.RedGhost.Size = new System.Drawing.Size(47, 47);
            this.RedGhost.TabIndex = 4;
            this.RedGhost.Text = "ghost1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1484, 1011);
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
            this.LivesContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LifeIcon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LifeIcon2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GameObjects.Player Player;
        private GameObjects.Maze Maze;
        private Panel MazeContainer;
        private Label label1;
        private FlowLayoutPanel LivesContainer;
        private PictureBox LifeIcon1;
        private PictureBox LifeIcon2;
        private GameObjects.Ghost RedGhost;
    }
}