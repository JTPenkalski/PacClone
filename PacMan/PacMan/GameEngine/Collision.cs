using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Contacts;

namespace GameEngine;

public class Collision
{
    public Collider Other { get; init; }
    public IReadOnlyCollection<Vector2> Points => (IReadOnlyCollection<Vector2>)points;
    public Vector2 Normal { get; init; }

    protected ICollection<Vector2> points = new List<Vector2>();

    public Collision(Collider receiver, Contact contact)
    {
        Other = receiver;

        Manifold manifold = contact.Manifold;
        contact.GetWorldManifold(out WorldManifold worldManifold);

        for (int i = 0; i < manifold.pointCount; i++)
            points.Add(worldManifold.points[i]);

        Vector2 normalStart = worldManifold.points[0] - 0.1f * worldManifold.normal;
        Vector2 normalEnd = worldManifold.points[0] + 0.1f * worldManifold.normal;

        Normal = normalEnd - normalStart;
    }
}