namespace GameEngine;

public class PathfindingGrid
{
    public readonly struct Index : IEquatable<Index>
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Index(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Index left, Index right) => left.Equals(right);

        public static bool operator !=(Index left, Index right) => !(left == right);

        public bool Equals(Index other) => X == other.X && Y == other.Y;

        public override bool Equals(object? obj) => obj is Index index && Equals(index);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"X = {X}, Y = {Y}";
    }

    public int Width => Nodes.GetLength(0);
    public int Height => Nodes.GetLength(1);
    private PathfindingNode[,] _nodes;
    public PathfindingNode[,] Nodes
    {
        get => _nodes;
        set
        {
            _nodes = value;
            freeNodes = _nodes.OfType<PathfindingNode>().Where(n => !n.IsObstacle).ToArray();
        }
    }

    private PathfindingNode[] freeNodes;

    public PathfindingGrid(int width, int height) : this(new PathfindingNode[width, height]) { }

    public PathfindingGrid(PathfindingNode[,] nodes)
    {
        _nodes = nodes;
        freeNodes = nodes.OfType<PathfindingNode>().Where(n => !n.IsObstacle).ToArray();
    }

    public PathfindingNode this[int x, int y] => Nodes[x, y];

    public void Reset()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Nodes[x, y].GCost = int.MaxValue;
                Nodes[x, y].HCost = 0;
                Nodes[x, y].Parent = null;
            }
        }
    }

    public virtual int GetCost(Index a, Index b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

    public virtual IEnumerable<PathfindingNode> GetNeighbors(Index gridIndex)
    {
        ICollection<PathfindingNode> neighbors = new List<PathfindingNode>(8);

        for (int x = gridIndex.X - 1; x <= gridIndex.X + 1; x++)
        {
            for (int y = gridIndex.Y - 1; y <= gridIndex.Y + 1; y++)
            {
                // Ignore current node
                if (x == gridIndex.X && y == gridIndex.Y)
                    continue;

                // Ignore corners
                if ((x == gridIndex.X - 1 && y == gridIndex.Y - 1) || // Bottom-Left
                    (x == gridIndex.X + 1 && y == gridIndex.Y - 1) || // Bottom-Right
                    (x == gridIndex.X - 1 && y == gridIndex.Y + 1) || // Top-Left
                    (x == gridIndex.X + 1 && y == gridIndex.Y + 1))   // Top-Right
                    continue;

                // Add anything else, within bounds, to the neighbors list
                if (x >= 0 && x < Width && y >= 0 && y < Height)
                    neighbors.Add(Nodes[x, y]);
            }
        }

        return neighbors;
    }

    public Index GetRandomIndex() => freeNodes[Random.Integer(freeNodes.Length)].GridIndex;
}