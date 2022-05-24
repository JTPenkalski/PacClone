using GameEngine;
using PacMan.GameObjects;
using PacMan.Mazes;

namespace PacMan.Components;

public class MazeCollider : Collider
{
    protected static readonly ISet<MazeObject> OBSTACLES = new HashSet<MazeObject>() { MazeObject.WALL, MazeObject.GHOST_WALL };

    public Maze Maze { get; init; }

    public MazeCollider(GameObject gameObject) : base(gameObject)
    {
        Maze = (Maze)gameObject;
    }

    public override IEnumerable<Collision> GetCollisions(Collider other)
    {
        Vector2Int playerCell = Maze.GetMazeCell(other.Bounds.Left + (other.Bounds.Width / 2), other.Bounds.Top + (other.Bounds.Height / 2));
        Vector2Int nextCell = Vector2Int.ZERO;

        // Rigidbody moving up
        if (other.AttachedRigidbody.Velocity.Y < 0)
        {
            nextCell = new(playerCell.X, Math.Max(playerCell.Y - 1, 0));
        }
        // Rigidbody moving down
        else if (other.AttachedRigidbody.Velocity.Y > 0)
        {
            nextCell = new(playerCell.X, Math.Min(playerCell.Y + 1, Maze.HEIGHT - 1));
        }
        // Rigidbody moving left
        else if (other.AttachedRigidbody.Velocity.X < 0)
        {
            nextCell = new(Math.Max(playerCell.X - 1, 0), playerCell.Y);
        }
        // Rigidbody moving right
        else if (other.AttachedRigidbody.Velocity.X > 0)
        {
            nextCell = new(Math.Min(playerCell.X + 1, Maze.WIDTH - 1), playerCell.Y);
        }

        // Determine if colliding with a wall
        Rectangle nextCellBounds = new(nextCell.X * Maze.CellWidth, nextCell.Y * Maze.CellHeight, Maze.CellWidth, Maze.CellHeight);

        if (OBSTACLES.Contains(Maze[nextCell.X, nextCell.Y]) && Rectangle.Intersect(other.Bounds, nextCellBounds) != Rectangle.Empty)
        {
            yield return new Collision(other);
        }
        else
        {
            yield break;
        }
    }
}