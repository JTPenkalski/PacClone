namespace GameEngine;

public abstract class Collider : Component
{
    public virtual Rectangle Bounds => new(Position, GameObject.Size);

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
    public Vector2 Offset { get; set; }
    public Vector2 Origin { get; set; }
    public Vector2 Position => Origin + Offset;

    public Collider(GameObject gameObject) : base(gameObject) { }

    public abstract IEnumerable<Collision> GetCollisions(Collider other);
}