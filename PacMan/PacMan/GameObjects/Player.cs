using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;
using PacMan.Components;

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

        collider.Radius = 15;

        rigidbody.Body.SetGravityScale(0f);
        rigidbody.Body.SetLinearDampling(0f);
        rigidbody.Body.SetType(BodyType.Dynamic);
        rigidbody.Collider = collider;

        keyboardController.MoveSpeed = 0.15f;
    }
}