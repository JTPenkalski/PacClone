using Box2D.NetStandard.Dynamics.Fixtures;
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
    }

    public float MoveSpeed { get; set; }
    public Vector2 AxialInput { get; protected set; }
    public Vector2 InitialDirection { get; set; }
    public Vector2 Direction { get; protected set; }

    protected readonly Rigidbody rigidbody;

    public override void Initialize()
    {
        AxialInput = InitialDirection;
    }

    public override void Update()
    {
        Vector2 input = GetInput();

        // Valid, new input this frame
        if (input != Vector2.Zero)
        {
            AxialInput = input;
        }
    }

    public override void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (rigidbody.Collider != null && AxialInput != Vector2.Zero)
        {
            RaycastHit hit = Physics.RayCast(GameObject.Transform.CenterPosition, AxialInput, 32f, new Fixture[] { rigidbody.Collider.Fixture });
            if (!hit)
            {
                Direction = AxialInput;
            }
        }

        rigidbody.Body.ApplyLinearImpulseToCenter(Direction * MoveSpeed);
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