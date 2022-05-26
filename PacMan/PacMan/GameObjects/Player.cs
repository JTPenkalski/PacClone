using GameEngine;
using PacMan.Components;

namespace PacMan.GameObjects;

public class Player : GameObject
{
    public const float BASE_SPEED = 0.125f;
    public const int COLLIDER_OFFSET = 6;
    public const int COLLIDER_SIZE = 19;

    private int score;

    public Player() : base()
    {
        Renderer renderer = AddComponent<Renderer>();

        BoxCollider collider = AddComponent<BoxCollider>();
        collider.Size = new Vector2(COLLIDER_SIZE, COLLIDER_SIZE);
        collider.Offset = new Vector2(COLLIDER_OFFSET, COLLIDER_OFFSET);

        Rigidbody rigidbody = AddComponent<Rigidbody>();
        rigidbody.Collider = collider;
        rigidbody.Trigger += OnTrigger;

        InputReceiver inputReceiver = AddComponent<InputReceiver>();
        inputReceiver.Speed = BASE_SPEED;
    }

    private void OnTrigger(IEnumerable<Collision> triggers)
    {
        foreach (Collision collision in triggers)
        {
            if (collision.Other.GameObject is Maze maze)
            {
                score += Maze.PELLET_VALUE;

                Vector2Int mazeCell = maze.GetMazeCell((int)(Transform.Position.X + Size.Width / 2), (int)(Transform.Position.Y + Size.Height / 2));
                maze.ClearCell(mazeCell.X, mazeCell.Y);
            }
        }
    }
}