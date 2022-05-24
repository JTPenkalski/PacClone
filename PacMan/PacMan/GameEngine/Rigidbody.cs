namespace GameEngine;

public class Rigidbody : Component
{
    public Collider? Collider { get; set; }
    public Vector2 Velocity { get; set; }

    protected IList<Collision> collisions = new List<Collision>();

    public Rigidbody(GameObject gameObject) : base(gameObject) { }

    public override void FixedUpdate()
    {
        ResolveCollisions();

        if (collisions.Count > 0)
        {
            Velocity = Vector2.ZERO;
        }

        GameObject.Transform.Translate(Time.FixedDeltaTime * Velocity);
    }

    protected virtual void ResolveCollisions()
    {
        collisions.Clear();

        if (Collider != null)
        {
            List<Collision> collisions = new();

            foreach (GameObject gameObject in Game.GameObjects)
            {
                if (gameObject != GameObject && gameObject.GetComponent<Rigidbody>() is Rigidbody other && other.Collider != null)
                {
                    collisions.AddRange(other.Collider.GetCollisions(Collider));
                }
            }
        }
    }
}