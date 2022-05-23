namespace GameEngine;

public class Collision
{
    public Rigidbody Other { get; init; }

    public Collision(Rigidbody other)
    {
        Other = other;
    }
}