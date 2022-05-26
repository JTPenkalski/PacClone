using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;
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

    public override void Initialize()
    {
        for (int y = 0; y < Maze.HEIGHT; y++)
        {
            for (int x = 0; x < Maze.WIDTH; x++)
            {
                MazeCell cell = Maze[x, y];
                Vector2 cellCenterWorldPos = cell.GetWorldPosition(Maze) + new Vector2(Maze.CellWidth / 2f, Maze.CellHeight / 2f);

                if (OBSTACLES.Contains(cell.Content))
                {
                    PolygonShape box = new();
                    box.SetAsBox(Maze.CellWidth / 2f, Maze.CellHeight / 2f, cellCenterWorldPos, 0f);

                    FixtureDef fixtureDef = new()
                    {
                        density = 1f,
                        friction = 0f,
                        isSensor = false,
                        restitution = 0f,
                        shape = box
                    };

                    AttachedRigidbody.Body.CreateFixture(fixtureDef);
                }
            }
        }
    }
}