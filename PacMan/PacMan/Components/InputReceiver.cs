using GameEngine;

namespace PacMan.Components;

public class InputReceiver : Component
{
    public InputReceiver(GameObject gameObject) : base(gameObject)
    {
        Rigidbody? rb = GameObject.GetComponent<Rigidbody>();
        if (rb == null)
            throw new Exception($"Component {nameof(InputReceiver)} expects component {nameof(Rigidbody)}.");
        rigidbody = rb;
    }

    public float Speed { get; set; }
    public Vector2 AxialInput { get; private set; }

    private readonly Rigidbody rigidbody;

    public override void Update()
    {
        AxialInput = GetInput();

        rigidbody.Velocity = Speed * AxialInput;
    }

    protected virtual Vector2 GetInput()
    {
        int horizontal = Input.GetHorizontalAxis();
        int vertical = Input.GetVerticalAxis();

        if (horizontal < 0) return Vector2.LEFT;
        else if (horizontal > 0) return Vector2.RIGHT;
        else if (vertical > 0) return Vector2.UP;
        else if (vertical < 0) return Vector2.DOWN;
        else return AxialInput;
    }
}