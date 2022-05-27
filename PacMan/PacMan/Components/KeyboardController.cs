using GameEngine;

namespace PacMan.Components;

public class KeyboardController : Component
{
    public KeyboardController(GameObject gameObject) : base(gameObject)
    {
        Rigidbody? rb = GameObject.GetComponent<Rigidbody>();
        if (rb == null)
            throw new Exception($"Component {nameof(KeyboardController)} expects component {nameof(Rigidbody)}.");
        rigidbody = rb;

        rigidbody.CollisionEnter += Rigidbody_CollisionEnter;
    }

    public float MoveSpeed { get; set; }
    public Vector2 AxialInput { get; protected set; }
    public Vector2 InitialDirection { get; set; }
    public Vector2 Direction { get; protected set; }

    protected bool enteringWall;
    protected Vector2 previousAxialInput;
    protected readonly Rigidbody rigidbody;

    public override void Initialize()
    {
        AxialInput = InitialDirection;
    }

    public override void Update()
    {
        Vector2 input = GetInput();

        // Valid, new input this frame
        if (input != Vector2.Zero && input != AxialInput)
        {
            previousAxialInput = AxialInput;
            AxialInput = input;
        }
    }

    public override void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Direction = AxialInput;

        if (enteringWall)
        {
            enteringWall = false;
            Direction = previousAxialInput;
        }

        rigidbody.Body.ApplyLinearImpulseToCenter(Direction * MoveSpeed);
    }

    protected virtual void Rigidbody_CollisionEnter(Collision collision)
    {
        if (Vector2.Dot(collision.Normal, rigidbody.Velocity) == -1f)
            enteringWall = true;
    }

    protected virtual Vector2 GetInput()
    {
        int horizontal = Input.GetHorizontalAxis();
        int vertical = Input.GetVerticalAxis();

        if (horizontal < 0) return -1 * Vector2.UnitX;
        else if (horizontal > 0) return Vector2.UnitX;
        else if (vertical > 0) return Vector2.UnitY;
        else if (vertical < 0) return -1 * Vector2.UnitY;
        else return Vector2.Zero;
    }
}