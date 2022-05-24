using GameEngine;
using PacMan.Components;

namespace PacMan.GameObjects;

public class Player : GameObject
{
    public const float BASE_SPEED = 0.125f;

    public Player() : base()
    {
        Renderer renderer = AddComponent<Renderer>();

        BoxCollider collider = AddComponent<BoxCollider>();
        collider.Size = new Vector2(21, 21);
        collider.Offset = new Vector2(5, 5);

        Rigidbody rigidbody = AddComponent<Rigidbody>();
        rigidbody.Collider = collider;

        InputReceiver inputReceiver = AddComponent<InputReceiver>();
        inputReceiver.Speed = BASE_SPEED;
    }
}