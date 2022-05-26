using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;

namespace GameEngine;

public abstract class Collider : Component
{
    public bool Trigger { get; set; }
    public Fixture Fixture => AttachedRigidbody.Body.GetFixtureList();
    public Rigidbody AttachedRigidbody { get; init; }
    public Shape Shape { get; protected set; }
    public Vector2 Offset { get; set; }
    public Vector2 Position => new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Y) + new Vector2(GameObject.Size.Width / 2f, GameObject.Size.Height / 2f) + Offset;

    public Collider(GameObject gameObject) : base(gameObject)
    {
        Rigidbody? rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
            throw new InvalidOperationException($"Component {nameof(Collider)} expects component {nameof(Rigidbody)}.");

        AttachedRigidbody = rigidbody;
        Shape = new PolygonShape();
    }

    public abstract override void Initialize();
}