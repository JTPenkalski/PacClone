using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;
using PacMan.Components;
using PacMan.Mazes;

namespace PacMan.GameObjects;

public class Ghost : GameObject
{
    protected Maze maze;
    protected readonly Animator animator;
    protected readonly CircleCollider collider;
    protected readonly Rigidbody rigidbody;

    public Ghost() : base()
    {
        renderer = AddComponent<Renderer>();
        animator = AddComponent<Animator>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<CircleCollider>();
    }

    protected override void InitLayout()
    {
        base.InitLayout();

        if (renderer != null)
            renderer.Sprite = Resources.RedGhost01;

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

        rigidbody.Body.SetFixedRotation(true);
        rigidbody.Body.SetGravityScale(0f);
        rigidbody.Body.SetLinearDampling(0f);
        rigidbody.Body.SetType(BodyType.Dynamic);
        rigidbody.Collider = collider;
        rigidbody.TriggerEnter += Rigidbody_TriggerEnter;

        maze = (Maze)Game.FindGameObject("Maze");
    }

    protected virtual void Rigidbody_TriggerEnter(Collision collision)
    {
        
    }
}