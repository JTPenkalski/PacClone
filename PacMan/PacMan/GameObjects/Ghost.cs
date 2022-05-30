using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;

namespace PacMan.GameObjects;

public class Ghost : GameObject
{
    protected Maze maze;
    protected readonly Animator animator;
    protected readonly CircleCollider collider;
    protected readonly PathfindingAgent pathfindingAgent;
    protected readonly Rigidbody rigidbody;

    public Ghost() : base()
    {
        renderer = AddComponent<Renderer>();
        animator = AddComponent<Animator>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<CircleCollider>();
        pathfindingAgent = AddComponent<PathfindingAgent>();

        renderer.Sprite = Resources.RedGhost01;
        maze = Game.FindGameObjectOfType<Maze>();
    }

    public override void OnStart()
    {
        Animation right = new($@"{Program.PROJECT_PATH}\Animations\RedGhost_Right.txt");
        Animation left = new($@"{Program.PROJECT_PATH}\Animations\RedGhost_Left.txt");
        Animation up = new($@"{Program.PROJECT_PATH}\Animations\RedGhost_Up.txt");
        Animation down = new($@"{Program.PROJECT_PATH}\Animations\RedGhost_Down.txt");

        animator.AddAnimation(right, true);
        animator.AddAnimation(left);
        animator.AddAnimation(up);
        animator.AddAnimation(down);

        animator.AddTrigger("RIGHT");
        animator.AddTrigger("LEFT");
        animator.AddTrigger("UP");
        animator.AddTrigger("DOWN");

        animator.AddTransition(Animation.Any, right, "RIGHT");
        animator.AddTransition(Animation.Any, left, "LEFT");
        animator.AddTransition(Animation.Any, up, "UP");
        animator.AddTransition(Animation.Any, down, "DOWN");

        collider.Radius = 16;

        rigidbody.Body.SetType(BodyType.Kinematic);
        rigidbody.Collider = collider;
        rigidbody.TriggerEnter += Rigidbody_TriggerEnter;

        pathfindingAgent.Grid = maze.PathfindingGrid;
        pathfindingAgent.Current = maze.GetPathfindingGridCell(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
    }

    protected virtual void Rigidbody_TriggerEnter(Collision collision)
    {
        
    }
}