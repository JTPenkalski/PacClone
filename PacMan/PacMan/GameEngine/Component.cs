namespace GameEngine;

public abstract class Component
{
    public bool Enabled { get; set; }
    public GameObject GameObject { get; init; }

    public Component(GameObject gameObject)
    {
        Enabled = true;
        GameObject = gameObject;

        Game.FixedUpdate += FixedUpdate;
        Game.Initialize += Initialize;
        Game.Update += Update;
    }

    ~Component()
    {
        Game.FixedUpdate -= FixedUpdate;
        Game.Initialize -= Initialize;
        Game.Update -= Update;
    }

    public virtual void Initialize() { }

    public virtual void FixedUpdate() { }

    public virtual void Update() { }
}