using Priority_Queue;

namespace GameEngine;

public class PathfindingAgent : Component
{
    public PathfindingNode? Current { get; set; }
    public PathfindingNode? Destination { get; protected set; }
    public PathfindingGrid? Grid { get; set; }

    protected int currentPathIndex;
    protected PathfindingNode[]? path;
    protected readonly Rigidbody rigidbody;

    public PathfindingAgent(GameObject gameObject) : base(gameObject)
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public override void FixedUpdate()
    {
        if (Grid != null && Current != null && Destination != null && path != null)
        {
            Vector2 centerPositionInt = new((int)rigidbody.GameObject.Transform.CenterPosition.X + 1, (int)rigidbody.GameObject.Transform.CenterPosition.Y + 1);

            if (centerPositionInt == path[currentPathIndex].WorldPosition)
            {
                // Generate new path
                if (path[currentPathIndex].GridIndex == Destination.GridIndex)
                {
                    PathfindingGrid.Index destinationIndex = Grid.GetRandomIndex();
                    while (destinationIndex == Destination.GridIndex)
                        destinationIndex = Grid.GetRandomIndex();

                    Current = Destination;
                    Destination = Grid[destinationIndex.X, destinationIndex.Y];

                    currentPathIndex = 0;
                    path = FindPath(Current, Destination);
                }
                // Next waypoint
                else
                {
                    Current = path[currentPathIndex];
                    currentPathIndex++;
                }
            }

            rigidbody.Velocity = path[currentPathIndex].WorldPosition - Current.WorldPosition;
        }
        // Repeatedly try to generate the initial path until Current is set
        else if (Grid != null && Current != null)
        {
            PathfindingGrid.Index destinationIndex = Grid.GetRandomIndex();
            Destination = Grid[destinationIndex.X, destinationIndex.Y];

            path = FindPath(Current, Destination);
        }
    }

    protected virtual int Heuristic(PathfindingNode a, PathfindingNode b) =>
        (int)(Math.Abs(a.WorldPosition.X - b.WorldPosition.X) + Math.Abs(a.WorldPosition.Y - b.WorldPosition.Y));

    protected virtual PathfindingNode[] FindPath(PathfindingNode startNode, PathfindingNode targetNode)
    {
        if (Grid == null)
            throw new InvalidOperationException($"Cannot find a new path because the {nameof(PathfindingGrid)} is null.");

        Grid.Reset();

        if (startNode.IsObstacle || targetNode.IsObstacle)
            return Array.Empty<PathfindingNode>();

        IPriorityQueue<PathfindingNode, int> openSet = new SimplePriorityQueue<PathfindingNode, int>();
        ISet<PathfindingNode> closedSet = new HashSet<PathfindingNode>();

        openSet.Enqueue(startNode, startNode.FCost);

        while (openSet.Count > 0)
        {
            PathfindingNode currentNode = openSet.Dequeue();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
                return RetracePath(startNode, targetNode);

            foreach (PathfindingNode n in Grid.GetNeighbors(currentNode.GridIndex))
            {
                if (!n.IsObstacle && !closedSet.Contains(n))
                {
                    int neighborGCost = currentNode.GCost + Grid.GetCost(currentNode.GridIndex, n.GridIndex);
                    if (neighborGCost < n.GCost)
                    {
                        n.GCost = neighborGCost;
                        n.HCost = Heuristic(n, targetNode);
                        n.Parent = currentNode;

                        if (!openSet.Contains(n))
                            openSet.Enqueue(n, n.FCost);
                    }
                }
            }
        }

        return Array.Empty<PathfindingNode>();
    }

    protected virtual PathfindingNode[] RetracePath(PathfindingNode startNode, PathfindingNode endNode)
    {
        // Retrace path to get all applicable nodes
        IList<PathfindingNode> path = new List<PathfindingNode>();
        PathfindingNode? nodeToAdd = endNode;

        while (nodeToAdd != null)
        {
            path.Add(nodeToAdd);
            nodeToAdd = nodeToAdd.Parent;
        }

        // Find the waypoints, but only where direction changes to reduce array size
        IList<PathfindingNode> waypoints = new List<PathfindingNode>();
        Vector2 currentDirection = Vector2.Zero;

        for (int i = 1; i < path.Count; i++)
        {
            // First minus Second because array is currently in reverse
            Vector2 direction = new(path[i - 1].WorldPosition.X - path[i].WorldPosition.X, path[i - 1].WorldPosition.Y - path[i].WorldPosition.Y);

            if (direction != currentDirection)
            {
                waypoints.Add(path[i - 1]);
                currentDirection = direction;
            }
        }

        return waypoints.Reverse().ToArray();
    }
}