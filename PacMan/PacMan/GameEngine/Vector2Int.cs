namespace GameEngine;

public readonly struct Vector2Int : IEquatable<Vector2Int>
{
    public static readonly Vector2Int DOWN = new(0, 1);
    public static readonly Vector2Int LEFT = new(-1, 0);
    public static readonly Vector2Int ONE = new(1, 1);
    public static readonly Vector2Int RIGHT = new(1, 0);
    public static readonly Vector2Int UP = new(0, -1);
    public static readonly Vector2Int ZERO = new(0, 0);

    public int X { get; init; }
    public int Y { get; init; }
    public float Magnitude => MathF.Sqrt((X * X) + (Y * Y));
    public float SquareMagnitude => (X * X) + (Y * Y);
    public Vector2 Normalized => new(X / Magnitude, Y / Magnitude);

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator Point(Vector2Int a) => new(a.X, a.Y);

    public static implicit operator Vector2Int(Point a) => new(a.X, a.Y);

    public static implicit operator Size(Vector2Int a) => new(a.X, a.Y);

    public static implicit operator Vector2Int(Size a) => new(a.Width, a.Height);

    public static implicit operator Vector2(Vector2Int a) => new(a.X, a.Y);

    public static implicit operator Vector2Int(Vector2 a) => new((int)a.X, (int)a.Y);

    public static Vector2Int operator *(int a, Vector2Int b) => new(a * b.X, a * b.Y);

    public static Vector2Int operator /(int a, Vector2Int b) => new(a / b.X, a / b.Y);

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2Int operator *(Vector2Int a, Vector2Int b) => new(a.X * b.X, a.Y * b.Y);

    public static Vector2Int operator /(Vector2Int a, Vector2Int b) => new(a.X / b.X, a.Y / b.Y);

    public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);

    public static bool operator !=(Vector2Int a, Vector2Int b) => !(a == b);

    public static float Angle(Vector2Int a, Vector2Int b) => MathF.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

    public static float Distance(Vector2Int a, Vector2Int b) => (a - b).Magnitude;

    public static float Dot(Vector2Int a, Vector2Int b) => a.X * b.X + a.Y * b.Y;

    public static Vector2Int Lerp(Vector2Int a, Vector2Int b, float t) => new((int)MathUtils.Lerp(a.X, b.X, t), (int)MathUtils.Lerp(a.Y, b.Y, t));

    public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => obj is Vector2Int vector && Equals(vector);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"<{X}, {Y}>";
}