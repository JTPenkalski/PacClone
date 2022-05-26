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
    public Vector2 AxialInput { get; private set; }

    private readonly Rigidbody rigidbody;

    public override void Update()
    {
        AxialInput = GetInput();

        if (AxialInput != Vector2.Zero)
            rigidbody.Body.ApplyLinearImpulseToCenter(AxialInput * MoveSpeed);
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