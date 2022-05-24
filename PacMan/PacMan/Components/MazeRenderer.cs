using GameEngine;
using PacMan.GameObjects;
using PacMan.Mazes;

namespace PacMan.Components;

public class MazeRenderer : Renderer
{
    protected static readonly ISet<MazeObject> VALID_WALLS = new HashSet<MazeObject>() { MazeObject.WALL, MazeObject.GHOST_WALL };
    protected static readonly IDictionary<int, int> IMAGE_MASK_REDUNDANCIES = new Dictionary<int, int>()
    {
        { 0, 47 },
        { 2, 1 },
        { 8, 2 }, 
        { 10, 3 }, 
        { 11, 4 }, 
        { 16, 5 }, 
        { 18, 6 }, 
        { 22, 7 }, 
        { 24, 8 }, 
        { 26, 9 }, 
        { 27, 10 },
        { 30, 11 }, 
        { 31, 12 }, 
        { 64, 13 }, 
        { 66, 14 }, 
        { 72, 15 }, 
        { 74, 16 }, 
        { 75, 17 },
        { 80, 18 }, 
        { 82, 19 }, 
        { 86, 20 }, 
        { 88, 21 }, 
        { 90, 22 },
        { 91, 23 }, 
        { 94, 24 }, 
        { 95, 25 }, 
        { 104, 26 }, 
        { 106, 27 }, 
        { 107, 28 }, 
        { 120, 29 }, 
        { 122, 30 }, 
        { 123, 31 }, 
        { 126, 32 }, 
        { 127, 33 }, 
        { 208, 34 }, 
        { 210, 35 }, 
        { 214, 36 }, 
        { 216, 37 }, 
        { 218, 38 }, 
        { 219, 39 }, 
        { 222, 40 }, 
        { 223, 41 }, 
        { 248, 42 }, 
        { 250, 43 }, 
        { 251, 44 }, 
        { 254, 45 }, 
        { 255, 46 }
    };
    protected static readonly IDictionary<int, Image> IMAGE_MASK = new Dictionary<int, Image>()
    {
        { 0, Resources.Wall_Individual },
        { 1, Resources.Wall_North },
        { 2, Resources.Wall_West },
        { 3, Resources.Wall_NorthWest },
        { 4, Resources.Wall_NorthWest },
        { 5, Resources.Wall_East },
        { 6, Resources.Wall_NorthEast },
        { 7, Resources.Wall_NorthEast },
        { 8, Resources.Wall_Horizontal },
        { 9, Resources.Wall_NotSouth },
        { 10, Resources.Wall_Horizontal },
        { 11, Resources.Wall_Horizontal },
        { 12, Resources.Wall_Horizontal },
        { 13, Resources.Wall_South },
        { 14, Resources.Wall_Vertical },
        { 15, Resources.Wall_SouthWest },
        { 16, Resources.Wall_NotEast },
        { 17, Resources.Wall_SouthWest },
        { 18, Resources.Wall_SouthEast },
        { 19, Resources.Wall_NotWest },
        { 20, Resources.Wall_SouthEast },
        { 21, Resources.Wall_NotNorth },
        { 22, Resources.Wall_Junction },
        { 23, Resources.Wall_Individual },
        { 24, Resources.Wall_Horizontal },
        { 25, Resources.Wall_SouthWest },
        { 26, Resources.Wall_SouthWest },
        { 27, Resources.Wall_NorthWest },
        { 28, Resources.Wall_Vertical },
        { 29, Resources.Wall_SouthEast },
        { 30, Resources.Wall_Vertical },
        { 31, Resources.Wall_NorthEast },
        { 32, Resources.Wall_SouthEast },
        { 33, Resources.Wall_SouthEast },
        { 34, Resources.Wall_SouthEast },
        { 35, Resources.Wall_NorthEast },
        { 36, Resources.Wall_Vertical },
        { 37, Resources.Wall_SouthWest },
        { 38, Resources.Wall_Individual },
        { 39, Resources.Wall_Vertical },
        { 40, Resources.Wall_Individual },
        { 41, Resources.Wall_SouthWest },
        { 42, Resources.Wall_Horizontal },
        { 43, Resources.Wall_Horizontal },
        { 44, Resources.Wall_NorthEast },
        { 45, Resources.Wall_NorthWest },
        { 46, Resources.Wall_Individual },
        { 47, Resources.Wall_Individual }
    };

    public Maze Maze => (Maze)GameObject;

    public MazeRenderer(GameObject gameObject) : base(gameObject) { }

    public override void Render(object? sender, PaintEventArgs e)
    {
        if (sender != GameObject)
            throw new ArgumentException("The specified sender does not match this Renderer's attached GameObject.");

        using BufferedGraphics buffer = graphicsContext.Allocate(e.Graphics, GameObject.ClientRectangle);

        int cellSizeX = Maze.Width / Maze.WIDTH;
        int cellSizeY = Maze.Height / Maze.HEIGHT;

        for (int y = 0; y < Maze.HEIGHT; y++)
        {
            for (int x = 0; x < Maze.WIDTH; x++)
            {
                if (Maze[x, y] != MazeObject.AIR)
                {
                    using Brush brush = GetMazeCellBrush(x, y, cellSizeX, cellSizeY);
                    buffer.Graphics.FillRectangle(brush, x * cellSizeX, y * cellSizeY, cellSizeX, cellSizeY);
                }
            }
        }

        buffer.Render();
    }

    protected virtual Brush GetMazeCellBrush(int x, int y, int sizeX, int sizeY)
    {
        return Maze[x, y] switch
        {
            MazeObject.WALL => new TextureBrush(ResizeImage(GetMazeCellWallImage(x, y), sizeX, sizeY)),
            MazeObject.PELLET => new SolidBrush(Color.White),
            MazeObject.POWER_PELLET => new SolidBrush(Color.DarkGray),
            MazeObject.GHOST_WALL => new SolidBrush(Color.Purple),
            _ => new SolidBrush(Color.Magenta)
        };
    }

    protected virtual Image GetMazeCellWallImage(int x, int y)
    {
        int north = y - 1 >= 0 && VALID_WALLS.Contains(Maze[x, y - 1]) ? 1 : 0;
        int east = x + 1 < Maze.WIDTH && VALID_WALLS.Contains(Maze[x + 1, y]) ? 1 : 0;
        int south = y + 1 < Maze.HEIGHT && VALID_WALLS.Contains(Maze[x, y + 1]) ? 1 : 0;
        int west = x - 1 >= 0 && VALID_WALLS.Contains(Maze[x - 1, y]) ? 1 : 0;

        int northWest = north == 1 && west == 1 && (x - 1 >= 0 && y - 1 >= 0) && VALID_WALLS.Contains(Maze[x - 1, y - 1]) ? 1 : 0;
        int northEast = north == 1 && east == 1 && (x + 1 < Maze.WIDTH && y - 1 >= 0) && VALID_WALLS.Contains(Maze[x + 1, y - 1]) ? 1 : 0;
        int southWest = south == 1 && west == 1 && (x - 1 >= 0 && y + 1 < Maze.HEIGHT) && VALID_WALLS.Contains(Maze[x - 1, y + 1]) ? 1 : 0;
        int southEast = south == 1 && east == 1 && (x + 1 < Maze.WIDTH && y + 1 < Maze.HEIGHT) && VALID_WALLS.Contains(Maze[x + 1, y + 1]) ? 1 : 0;

        int rawIndex = northWest + 2 * north + 4 * northEast + 8 * west + 16 * east + 32 * southWest + 64 * south + 128 * southEast;
        int index = IMAGE_MASK_REDUNDANCIES.ContainsKey(rawIndex) ? IMAGE_MASK_REDUNDANCIES[rawIndex] : rawIndex;

        return IMAGE_MASK[index];
    }
}