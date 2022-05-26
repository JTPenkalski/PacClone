namespace GameEngine;

public readonly struct Vector2 : IEquatable<Vector2>
{
    public static readonly Vector2 DOWN = new(0f, 1f);
    public static readonly Vector2 LEFT = new(-1f, 0f);
    public static readonly Vector2 ONE = new(1f, 1f);
    public static readonly Vector2 RIGHT = new(1f, 0f);
    public static readonly Vector2 UP = new(0f, -1f);
    public static readonly Vector2 ZERO = new(0f, 0f);
    
    public float X { get; init; }
    public float Y { get; init; }
    public float Magnitude => MathF.Sqrt((X * X) + (Y * Y));
    public float SquareMagnitude => (X * X) + (Y * Y);
    public Vector2 Normalized => new(X / Magnitude, Y / Magnitude);

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator Point(Vector2 a) => new((int)a.X, (int)a.Y);

    public static implicit operator Vector2(Point a) => new(a.X, a.Y);

    public static implicit operator Size(Vector2 a) => new((int)a.X, (int)a.Y);

    public static implicit operator Vector2(Size a) => new(a.Width, a.Height);

    public static implicit operator Vector2(Vector2Int a) => new(a.X, a.Y);

    public static implicit operator Vector2Int(Vector2 a) => new((int)a.X, (int)a.Y);

    public static Vector2 operator *(float a, Vector2 b) => new(a * b.X, a * b.Y);

    public static Vector2 operator /(float a, Vector2 b) => new(a / b.X, a / b.Y);

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

    public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);

    public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);

    public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

    public static float Angle(Vector2 a, Vector2 b) => MathF.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

    public static float Distance(Vector2 a, Vector2 b) => (a - b).Magnitude;

    public static float Dot(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;

    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => new(MathUtils.Lerp(a.X, b.X, t), MathUtils.Lerp(a.Y, b.Y, t));

    public bool Equals(Vector2 other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => obj is Vector2 vector && Equals(vector);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"<{X}, {Y}>";
}