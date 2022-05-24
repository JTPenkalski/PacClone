namespace GameEngine;

public class Collision
{
    public Collider Other { get; init; }

    public Collision(Collider other)
    {
        Other = other;
    }
}