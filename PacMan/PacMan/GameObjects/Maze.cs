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

    public int CellWidth => Width / WIDTH;

    public int CellHeight => Height / HEIGHT;

    public MazeCell this[int x, int y] => new(x, y, (MazeObject)contents[x, y]);

    protected readonly int[,] contents;
    protected readonly MazeCollider collider;
    protected readonly Rigidbody rigidbody;

    public Maze() : base()
    {
        contents = new int[WIDTH, HEIGHT];
        SetMaze("Maze01");

        renderer = AddComponent<MazeRenderer>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<MazeCollider>();

        GameManager.LevelChanged += SetMaze;
    }

    protected override void InitLayout()
    {
        base.InitLayout();

        rigidbody.Body.SetType(BodyType.Static);
        rigidbody.Collider = collider;
    }

    public void ClearCell(int cellX, int cellY)
    {
        contents[cellX, cellY] = (int)MazeObject.AIR;
        Invalidate(new Rectangle(cellX * CellWidth, cellY * CellHeight, CellWidth, CellHeight));
    }

    public void SetMaze(string mazeName)
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
}