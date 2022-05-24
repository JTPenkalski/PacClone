using GameEngine;
using PacMan.Components;
using PacMan.Mazes;

namespace PacMan.GameObjects;

public class Maze : GameObject
{
    public const int HEIGHT = 31;
    public const int WIDTH = 28;

    public int CellWidth => Width / WIDTH;

    public int CellHeight => Height / HEIGHT;

    public MazeObject this[int x, int y] => (MazeObject)contents[x, y];

    private readonly int[,] contents;

    public Maze() : base()
    {
        contents = new int[WIDTH, HEIGHT];
        SetMaze("Maze01");

        MazeRenderer renderer = AddComponent<MazeRenderer>();

        MazeCollider collider = AddComponent<MazeCollider>();

        Rigidbody rigidbody = AddComponent<Rigidbody>();
        rigidbody.Collider = collider;
        rigidbody.Static = true;

        GameManager.LevelChanged += SetMaze;
    }

    public void SetMaze(string mazeName)
    {
        string[] values = File.ReadAllText($@"D:\My Projects\C# Projects\PacMan\PacMan\PacMan\bin\Debug\net6.0-windows\Mazes\{mazeName}.txt").Split(',');
        
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                contents[x, y] = int.Parse(values[y * WIDTH + x]);
            }
        }
    }

    public Vector2Int GetMazeCell(int x, int y)
    {
        return new Vector2Int(x / CellWidth, y / CellHeight);
    }
}