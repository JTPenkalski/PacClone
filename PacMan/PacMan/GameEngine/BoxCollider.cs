namespace GameEngine;

public class BoxCollider : Collider
{
    public override Rectangle Bounds => new(Position + (SkinWidth * Vector2.ONE), Size);

    public int SkinWidth { get; set; }
    private Vector2 _size;
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;

            _size -= 2 * SkinWidth * Vector2.ONE;
        }
    }

    public BoxCollider(GameObject gameObject) : base(gameObject) { }

    public override void Initialize()
    {
        base.Initialize();

        if (Size.X == 0 && Size.Y == 0)
            Size = GameObject.Size;
    }

    public override IEnumerable<Collision> GetCollisions(Collider other)
    {
        if (Bounds.IntersectsWith(other.Bounds))
        {
            yield return new Collision(this, Vector2.ZERO);
        }
    }
}