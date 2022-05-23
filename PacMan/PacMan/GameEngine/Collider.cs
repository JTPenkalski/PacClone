namespace GameEngine;

public abstract class Collider : Component
{
    public Rectangle Bounds => new((Vector2)GameObject.Location + Offset, GameObject.Size);
    private Rigidbody? _attachedRigidbody;
    public Rigidbody AttachedRigidbody
    {
        get
        {
            if (_attachedRigidbody != null)
                return _attachedRigidbody;

            _attachedRigidbody = GameObject.GetComponent<Rigidbody>();
            return _attachedRigidbody;
        }
    }
    public Vector2 Position => GameObject.Transform.Position + (Vector2)(GameObject.Size / 2) + Offset;
    public Vector2 Offset { get; set; }

    public Collider(GameObject gameObject) : base(gameObject) { }

    public abstract IEnumerable<Collision> GetCollisions(Collider other);
}