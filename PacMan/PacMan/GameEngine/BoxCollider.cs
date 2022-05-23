namespace GameEngine;

public class BoxCollider : Collider
{
    public BoxCollider(GameObject gameObject) : base(gameObject) { }

    public override IEnumerable<Collision> GetCollisions(Collider other)
    {
        if (Bounds.IntersectsWith(other.Bounds))
        {
            yield return new Collision(other.AttachedRigidbody);
        }
    }
}