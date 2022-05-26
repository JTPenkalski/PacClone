using Box2D.NetStandard.Dynamics.Bodies;
using GameEngine;
using PacMan.Components;

namespace PacMan.GameObjects;

public class Player : GameObject
{
    protected int score;
    protected readonly Renderer renderer;
    protected readonly CircleCollider collider;
    protected readonly Rigidbody rigidbody;
    protected readonly KeyboardController keyboardController;

    public Player() : base()
    {
        renderer = AddComponent<Renderer>();
        rigidbody = AddComponent<Rigidbody>();
        collider = AddComponent<CircleCollider>();
        keyboardController = AddComponent<KeyboardController>();
    }

    protected override void InitLayout()
    {
        base.InitLayout();

        collider.Radius = 15;

        rigidbody.Body.SetGravityScale(0f);
        rigidbody.Body.SetLinearDampling(0f);
        rigidbody.Body.SetType(BodyType.Dynamic);
        rigidbody.Collider = collider;

        keyboardController.MoveSpeed = 0.15f;
    }
}