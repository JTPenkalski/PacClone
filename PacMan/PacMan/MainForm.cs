namespace PacMan
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            MazeContainer.Width = Width / 2;
        }
    }
}