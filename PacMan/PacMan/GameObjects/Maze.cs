using GameEngine;
using PacMan.Components;

namespace PacMan.GameObjects;

public class Maze : GameObject
{
    public const int HEIGHT = 31;
    public const int WIDTH = 28;

    public int this[int x, int y] => contents[x, y];

    private readonly int[,] contents;

    public Maze() : base()
    {
        contents = new int[WIDTH, HEIGHT];
        SetMaze("Maze01");

        MazeRenderer renderer = AddComponent<MazeRenderer>();

        BoxCollider collider = AddComponent<BoxCollider>();

        Rigidbody rigidbody = AddComponent<Rigidbody>();
        rigidbody.Collider = collider;

        AspectRatioFitter aspectRatioFitter = AddComponent<AspectRatioFitter>();
        aspectRatioFitter.AspectRatio = 0.861f;

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
}