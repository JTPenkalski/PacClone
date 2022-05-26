using GameEngine;
using PacMan.GameObjects;
using PacMan.Mazes;

namespace PacMan.Components;

public class MazeCollider : Collider
{
    protected static readonly ISet<MazeObject> OBSTACLES = new HashSet<MazeObject>() { MazeObject.WALL, MazeObject.GHOST_WALL };
    protected static readonly ISet<MazeObject> TRIGGERS = new HashSet<MazeObject>() { MazeObject.PELLET, MazeObject.POWER_PELLET };

    public Maze Maze => (Maze)GameObject;

    public MazeCollider(GameObject gameObject) : base(gameObject) { }

    public override IEnumerable<Collision> GetCollisions(Collider other)
    {
        ICollection<Collision> result = new HashSet<Collision>();

        foreach (Collision collision in GetObstacleCollisions(other))
            result.Add(collision);

        foreach (Collision trigger in GetTriggers(other))
            result.Add(trigger);

        return result;
    }

    protected virtual IEnumerable<Collision> GetObstacleCollisions(Collider other)
    {
        ISet<Vector2Int> nextCells = new HashSet<Vector2Int>(2);

        // Rigidbody moving up
        if (other.AttachedRigidbody.Velocity.Y < 0)
        {
            //nextCell = new(playerCell.X, Math.Max(playerCell.Y - 1, 0));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Left, other.Bounds.Top));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Right, other.Bounds.Top));
        }
        // Rigidbody moving down
        else if (other.AttachedRigidbody.Velocity.Y > 0)
        {
            //nextCell = new(playerCell.X, Math.Min(playerCell.Y + 1, Maze.HEIGHT - 1));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Left, other.Bounds.Bottom));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Right, other.Bounds.Bottom));
        }
        // Rigidbody moving left
        else if (other.AttachedRigidbody.Velocity.X < 0)
        {
            //nextCell = new(Math.Max(playerCell.X - 1, 0), playerCell.Y);
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Left, other.Bounds.Top));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Left, other.Bounds.Bottom));
        }
        // Rigidbody moving right
        else if (other.AttachedRigidbody.Velocity.X > 0)
        {
            //nextCell = new(Math.Min(playerCell.X + 1, Maze.WIDTH - 1), playerCell.Y);
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Right, other.Bounds.Top));
            nextCells.Add(Maze.GetMazeCell(other.Bounds.Right, other.Bounds.Bottom));
        }

        // Determine if colliding with a wall
        foreach (Vector2Int nextCell in nextCells)
        {
            Rectangle nextCellBounds = new(nextCell.X * Maze.CellWidth, nextCell.Y * Maze.CellHeight, Maze.CellWidth, Maze.CellHeight);

            if (OBSTACLES.Contains(Maze[nextCell.X, nextCell.Y]))
            {
                Rectangle intersection = Rectangle.Intersect(other.Bounds, nextCellBounds);
                Vector2 collisionDepth = intersection.Size * other.AttachedRigidbody.Velocity.Normalized;
                
                if (collisionDepth != Vector2.ZERO)
                {
                    Debug.WriteLine("Player B = " + other.Bounds);
                    Debug.WriteLine("Intersection = " + intersection);
                    Debug.WriteLine("Next Cell = " + nextCell);
                    Debug.WriteLine(new Vector2(intersection.Size.Width, intersection.Size.Height) * other.AttachedRigidbody.Velocity.Normalized);

                    yield return new Collision(this, new Vector2(intersection.Size.Width, intersection.Size.Height) * other.AttachedRigidbody.Velocity.Normalized);
                }
            }
        }
    }

    protected virtual IEnumerable<Collision> GetTriggers(Collider other)
    {
        Vector2Int playerCell = Maze.GetMazeCell(other.Bounds.Left + (other.Bounds.Width / 2), other.Bounds.Top + (other.Bounds.Height / 2));

        if (TRIGGERS.Contains(Maze[playerCell.X, playerCell.Y]))
        {
            yield return new Collision(this, Vector2.ZERO, true);
        }
    }
}