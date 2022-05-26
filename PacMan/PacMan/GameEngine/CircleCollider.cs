using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;

namespace GameEngine;

public class CircleCollider : Collider
{
    public float Radius { get; set; }

    public CircleCollider(GameObject gameObject) : base(gameObject) { }

    public override void Initialize()
    {
        CircleShape circle = new();
        circle.Set(new Vector2(GameObject.Size.Width / 2f, GameObject.Size.Height / 2f) + Offset, Radius);

        FixtureDef fixtureDef = new()
        {
            density = 1f,
            friction = 0f,
            isSensor = Trigger,
            shape = circle
        };

        AttachedRigidbody.Body.CreateFixture(fixtureDef);
    }
}