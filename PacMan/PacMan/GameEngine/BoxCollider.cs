using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;

namespace GameEngine;

public class BoxCollider : Collider
{
    public Vector2 Size { get; set; }

    public BoxCollider(GameObject gameObject) : base(gameObject) { }

    public override void Initialize()
    {
        if (Size.X == 0 && Size.Y == 0)
            Size = new Vector2(GameObject.Size.Width, GameObject.Size.Height);

        PolygonShape box = new();
        box.SetAsBox(Size.X / 2f, Size.Y / 2f, new Vector2(GameObject.Size.Width / 2f, GameObject.Size.Height / 2f) + Offset, 0f);

        FixtureDef fixtureDef = new()
        {
            density = 1f,
            friction = 0f,
            isSensor = Trigger,
            restitution = 0f,
            shape = box
        };

        AttachedRigidbody.Body.CreateFixture(fixtureDef);
    }
}