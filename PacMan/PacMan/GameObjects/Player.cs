using GameEngine;
using PacMan.Components;

namespace PacMan.GameObjects;

public class Player : GameObject
{
    public Player() : base()
    {
        Renderer renderer = AddComponent<Renderer>();

        CircleCollider collider = AddComponent<CircleCollider>();
        collider.Radius = Size.Width * 0.5f;

        Rigidbody rigidbody = AddComponent<Rigidbody>();
        rigidbody.Collider = collider;
        rigidbody.Velocity = new(0.5f, 0f);
    }
}