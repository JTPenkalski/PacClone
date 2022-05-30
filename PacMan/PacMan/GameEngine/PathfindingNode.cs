namespace GameEngine;

public class PathfindingNode : IComparable<PathfindingNode>, IEquatable<PathfindingNode>
{
    public bool IsObstacle { get; init; }
    public int GCost { get; set; }
    public int FCost => GCost + HCost;
    public int HCost { get; set; }
    public PathfindingNode? Parent { get; set; }
    public PathfindingGrid.Index GridIndex { get; init; }
    public Vector2 WorldPosition { get; init; }

    public static bool operator ==(PathfindingNode? a, PathfindingNode? b)
    {
        if (a is null)
            return b is null;

        return a.Equals(b);
    }
    public static bool operator !=(PathfindingNode? a, PathfindingNode? b) => !(a == b);

    public PathfindingNode(PathfindingGrid.Index gridIndex, Vector2 worldPos, bool isObstacle)
    {
        GridIndex = gridIndex;
        WorldPosition = worldPos;
        IsObstacle = isObstacle;
        GCost = int.MaxValue;
    }

    public bool Equals(PathfindingNode? other)
    {
        // If parameter is null, return false
        if (other is null)
            return false;

        // Optimization for a common success case
        if (ReferenceEquals(this, other))
            return true;

        // If runtime types are not exactly the same, return false
        if (GetType() != other.GetType())
            return false;

        // Perform the actual equality check
        return IsObstacle == other.IsObstacle && WorldPosition == other.WorldPosition;
    }

    public int CompareTo(PathfindingNode? other)
    {
        if (other == null)
            return 1;

        int compareFCosts = FCost.CompareTo(other.FCost);
        return compareFCosts != 0 ? compareFCosts : HCost.CompareTo(other.HCost);
    }

    public override bool Equals(object? obj) => Equals(obj as PathfindingNode);

    public override int GetHashCode() => HashCode.Combine(IsObstacle, GCost, HCost, WorldPosition);
}