namespace GameEngine;

public class Rigidbody : Component
{
    public event Action<IEnumerable<Collision>>? Collision;
    public event Action<IEnumerable<Collision>>? Trigger;

    public bool Static { get; set; }
    public Collider? Collider { get; set; }
    public Vector2 Velocity { get; set; }

    protected ICollection<Collision> hits = new HashSet<Collision>();

    public Rigidbody(GameObject gameObject) : base(gameObject) { }

    public override void FixedUpdate()
    {
        if (!Static)
        {
            if (Collider != null)
            {
                ResolveCollisions();

                IEnumerable<Collision> collisions = hits.Where(c => !c.Trigger);
                IEnumerable<Collision> triggers = hits.Where(c => c.Trigger);

                if (collisions.Any())
                {
                    foreach (Collision c in collisions)
                    {
                        GameObject.Transform.Translate(Vector2.LEFT * c.Depth);
                        GameObject.Transform.Translate(Vector2.UP * c.Depth);
                    }

                    OnCollision(collisions);
                    Velocity = Vector2.ZERO;
                }

                if (triggers.Any())
                {
                    OnTrigger(triggers);
                }
            }

            GameObject.Transform.Translate(Time.FixedDeltaTime * Velocity);
        }
    }

    protected virtual void OnCollision(IEnumerable<Collision> collisions) => Collision?.Invoke(collisions);

    protected virtual void OnTrigger(IEnumerable<Collision> triggers) => Trigger?.Invoke(triggers);

    protected virtual void ResolveCollisions()
    {
        if (Collider != null)
        {
            hits.Clear();

            Collider.Origin = GameObject.Transform.Position;

            foreach (GameObject gameObject in Game.GameObjects)
            {
                if (gameObject != GameObject && gameObject.GetComponent<Rigidbody>() is Rigidbody other && other.Collider != null)
                {
                    foreach (Collision c in other.Collider.GetCollisions(Collider))
                        hits.Add(c);
                }
            }
        }
    }
}