namespace GameEngine;

public class Rigidbody : Component
{
    public bool Static { get; set; }
    public Collider? Collider { get; set; }
    public Vector2 Velocity { get; set; }

    protected IList<Collision> collisions = new List<Collision>();

    public Rigidbody(GameObject gameObject) : base(gameObject) { }

    public override void FixedUpdate()
    {
        if (!Static)
        {
            if (Collider != null)
            {
                ResolveCollisions();

                if (collisions.Count > 0)
                {
                    Velocity = Vector2.ZERO;
                }
            }

            GameObject.Transform.Translate(Time.FixedDeltaTime * Velocity);
        }
    }

    protected virtual void ResolveCollisions()
    {
        if (Collider != null)
        {
            collisions.Clear();

            Collider.Origin = GameObject.Transform.Position;

            foreach (GameObject gameObject in Game.GameObjects)
            {
                if (gameObject != GameObject && gameObject.GetComponent<Rigidbody>() is Rigidbody other && other.Collider != null)
                {
                    foreach (Collision c in other.Collider.GetCollisions(Collider))
                        collisions.Add(c);
                }
            }
        }
    }
}