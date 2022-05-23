using GameEngine;
using PacMan.GameObjects;
using PacMan.Mazes;

namespace PacMan.Components;

public class MazeRenderer : Renderer
{
    public Maze Maze => (Maze)GameObject;

    public MazeRenderer(GameObject gameObject) : base(gameObject) { }

    public override void Render(object? sender, PaintEventArgs e)
    {
        if (sender != GameObject)
            throw new ArgumentException("The specified sender does not match this Renderer's attached GameObject.");

        using BufferedGraphics buffer = graphicsContext.Allocate(e.Graphics, GameObject.ClientRectangle);

        float cellSizeX = (float)Maze.Width / Maze.WIDTH;
        float cellSizeY = (float)Maze.Height / Maze.HEIGHT;

        for (int x = 0; x < Maze.WIDTH; x++)
        {
            for (int y = 0; y < Maze.HEIGHT; y++)
            {
                using Brush brush = GetMazeCellBrush((MazeObject)Maze[x, y], (int)cellSizeX, (int)cellSizeY);
                buffer.Graphics.FillRectangle(brush, x * cellSizeX, y * cellSizeY, cellSizeX, cellSizeY);
            }
        }

        buffer.Render();
    }

    protected virtual Brush GetMazeCellBrush(MazeObject mazeObj, int sizeX, int sizeY)
    {
        Brush brush = mazeObj switch
        {
            MazeObject.AIR => new SolidBrush(Color.Black),
            MazeObject.WALL => new TextureBrush(ResizeImage(Resources.Wall_Vertical, sizeX, sizeY)),
            MazeObject.PELLET => new SolidBrush(Color.White),
            MazeObject.POWER_PELLET => new SolidBrush(Color.DarkGray),
            MazeObject.GHOST_WALL => new SolidBrush(Color.Purple),
            _ => new SolidBrush(Color.Magenta)
        };

        return brush;
    }
}