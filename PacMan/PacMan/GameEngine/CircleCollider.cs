namespace GameEngine;

public class CircleCollider : Collider
{
    public float Radius { get; set; }

    public CircleCollider(GameObject gameObject) : base(gameObject) { }

    public override IEnumerable<Collision> GetCollisions(Collider other)
    {
        float detectionRadius = other is CircleCollider cc ? Radius + cc.Radius : Radius;

        if ((other.Position - Position).SquareMagnitude <= detectionRadius * detectionRadius)
        {
            yield return new Collision(other);
        }
    }
}