using GameEngine;

namespace PacMan.Components;

public class TestComponent : Component
{
    public TestComponent(GameObject gameObject) : base(gameObject) { }

    public override void Update()
    {
        GameObject.Transform.Translate(0.5f * Time.DeltaTime * Vector2.RIGHT);
    }
}