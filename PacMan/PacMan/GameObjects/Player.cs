using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;
using PacMan.Components;
using PacMan.Mazes;

namespace PacMan.GameObjects;

public class Player : GameObject
{
    protected int score;
    protected readonly Animator animator;
    protected readonly AnimationAligner animationAligner;
    protected readonly CircleCollider collider;
    protected readonly KeyboardController keyboardController;
    protected readonly Renderer renderer;
    protected readonly Rigidbody rigidbody;

    public Player() : base()
    {
        renderer = AddComponent<Renderer>();
        animator = AddComponent<Animator>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<CircleCollider>();
        keyboardController = AddComponent<KeyboardController>();
        animationAligner = AddComponent<AnimationAligner>();
    }

    protected override void InitLayout()
    {
        base.InitLayout();

        renderer.Sprite = Resources.Pac01;

        animator.AddAnimation(new Animation($@"{Program.PROJECT_PATH}\Animations\Player_Move.txt"), true);

        collider.Radius = 16;

        rigidbody.Body.SetFixedRotation(true);
        rigidbody.Body.SetGravityScale(0f);
        rigidbody.Body.SetLinearDampling(0f);
        rigidbody.Body.SetType(BodyType.Dynamic);
        rigidbody.Collider = collider;
        rigidbody.CollisionEnter += Rigidbody_CollisionEnter;
        rigidbody.TriggerEnter += Rigidbody_TriggerEnter;

        //keyboardController.InitialDirection = Vector2.UnitX;
        keyboardController.MoveSpeed = 1500f;
    }

    protected virtual void Rigidbody_CollisionEnter(Collision collision)
    {
        //Debug.WriteLine("Hit");
    }

    protected virtual void Rigidbody_TriggerEnter(Collision collision)
    {
        if (collision.Other.GameObject is Maze maze)
        {
            score += Maze.PELLET_VALUE;

            Vector2 playerCenterPos = new Vector2(Transform.Position.X + (Size.Width / 2f), Transform.Position.Y + (Size.Height / 2f));
            MazeCell cell = maze.GetMazeCell((int)playerCenterPos.X, (int)playerCenterPos.Y);

            maze.ClearCell(cell.X, cell.Y);
        }
    }
}