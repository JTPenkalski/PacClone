namespace GameEngine;

public class Collision
{
    public bool Trigger { get; init; }
    public Collider Other { get; init; }

    public Collision(Collider other, bool trigger = false)
    {
        Other = other;
        Trigger = trigger;
    }
}