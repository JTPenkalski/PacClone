namespace GameEngine;

public class Transform : Component
{
    public float Rotation { get; set; }
    public int ChildCount => Children.Count;
    public IReadOnlyList<Transform> Children => (IReadOnlyList<Transform>)children;
    public Vector2 CenterPosition => Position + new Vector2(GameObject.Size.Width / 2f, GameObject.Size.Height / 2f);
    public Vector2 Position { get; set; }
    public Transform? Parent { get; set; }

    private readonly IList<Transform> children = new List<Transform>();

    public Transform(GameObject gameObject) : base(gameObject) { }

    public override void Initialize()
    {
        Position = new Vector2(GameObject.Location.X, GameObject.Location.Y);
    }

    public override void Update()
    {
        GameObject.Location = new Point((int)Position.X, (int)Position.Y);
    }

    public void Translate(Vector2 displacement) => Position += displacement;

    public Transform GetChild(int index) => children[index];
}