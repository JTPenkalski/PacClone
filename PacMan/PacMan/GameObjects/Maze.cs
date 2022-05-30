using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;
using PacMan.Components;
using PacMan.Mazes;

namespace PacMan.GameObjects;

public class Maze : GameObject
{
    public const int HEIGHT = 31;
    public const int PELLET_VALUE = 100;
    public const int WIDTH = 28;
    protected static readonly ISet<MazeObject> PATH_OBSTACLES = new HashSet<MazeObject>() { MazeObject.WALL, MazeObject.GHOST_WALL, MazeObject.BARRIER };

    public int CellWidth => Size.Width / WIDTH;

    public int CellHeight => Size.Height / HEIGHT;

    public PathfindingGrid PathfindingGrid { get; protected set; }

    protected readonly int[,] contents;
    protected readonly MazeCollider collider;
    protected readonly Rigidbody rigidbody;

    public Maze() : base()
    {
        contents = new int[WIDTH, HEIGHT];
        PathfindingGrid = new(WIDTH, HEIGHT);
        SetMaze("Maze01");

        renderer = AddComponent<MazeRenderer>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<MazeCollider>();

        GameManager.LevelChanged += SetMaze;
    }

    public MazeCell this[int x, int y] => new(x, y, (MazeObject)contents[x, y]);

    public override void OnStart()
    {
        GeneratePathfindingGrid();

        rigidbody.Body.SetType(BodyType.Static);
        rigidbody.Collider = collider;
    }

    public void ClearCell(int cellX, int cellY)
    {
        contents[cellX, cellY] = (int)MazeObject.AIR;
        Invalidate(new Rectangle(cellX * CellWidth, cellY * CellHeight, CellWidth, CellHeight));
    }

    public virtual void SetMaze(string mazeName)
    {
        string[] values = File.ReadAllText($@"{Program.PROJECT_PATH}\Mazes\{mazeName}.txt").Split(',');

        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                contents[x, y] = int.Parse(values[y * WIDTH + x]);
            }
        }
    }

    public MazeCell GetMazeCell(int worldX, int worldY) => this[worldX / CellWidth, worldY / CellHeight];

    public PathfindingNode GetPathfindingGridCell(int worldX, int worldY) => PathfindingGrid[worldX / CellWidth, worldY / CellHeight];

    protected virtual void GeneratePathfindingGrid()
    {
        PathfindingNode[,] nodes = new PathfindingNode[WIDTH, HEIGHT];

        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                nodes[x, y] = new PathfindingNode
                (
                    new PathfindingGrid.Index(x, y),
                    new Vector2(x * CellWidth + CellWidth / 2, y * CellHeight + CellHeight / 2),
                    PATH_OBSTACLES.Contains(this[x, y].Content)
                );
            }
        }

        PathfindingGrid.Nodes = nodes;
    }
}