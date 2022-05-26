using PacMan.GameObjects;

namespace PacMan.Mazes;

public readonly struct MazeCell : IEquatable<MazeCell>
{
    public int X { get; init; }
    public int Y { get; init; }
    public MazeObject Content { get; init; }

    public MazeCell(int x, int y, MazeObject content)
    {
        X = x;
        Y = y;
        Content = content;
    }

    public static bool operator ==(MazeCell left, MazeCell right) => left.Equals(right);

    public static bool operator !=(MazeCell left, MazeCell right) => !(left == right);

    public static implicit operator Point(MazeCell a) => new(a.X, a.Y);

    public static implicit operator Size(MazeCell a) => new(a.X, a.Y);

    public static implicit operator Vector2(MazeCell a) => new(a.X, a.Y);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        return Equals((MazeCell)obj);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public bool Equals(MazeCell other)
    {
        return X == other.X && Y == other.Y;
    }

    public Vector2 GetWorldPosition(Maze maze)
    {
        return new Vector2(X * maze.CellWidth, Y * maze.CellHeight) + new Vector2(maze.Location.X, maze.Location.Y);
    }
}