using Box2D.NetStandard.Dynamics.Bodies;

namespace GameEngine;

public class Rigidbody : Component
{
    public event Action<Collision>? CollisionEnter;
    public event Action<Collision>? CollisionExit;
    public event Action<Collision>? TriggerEnter;
    public event Action<Collision>? TriggerExit;

    public Body Body { get; protected set; }
    public Collider? Collider { get; set; }
    public Vector2 Position => Body.GetPosition();
    public Vector2 Velocity
    {
        get => Body.GetLinearVelocity();
        set => Body.SetLinearVelocity(value);
    }

    protected Vector2 prevPosition;

    public Rigidbody(GameObject gameObject) : base(gameObject)
    {
        // Create a temporary Body property for the client to configure
        Body = Game.PhysicsWorld.CreateBody(new BodyDef());
    }

    public override void Initialize()
    {
        prevPosition = GameObject.Transform.Position;

        // Override the temporary Body property with position of Transform
        BodyDef bodyDef = new()
        {
            allowSleep = Body.IsSleepingAllowed(),
            angle = Body.GetAngle(),
            angularDamping = Body.GetAngularDamping(),
            angularVelocity = Body.GetAngularVelocity(),
            awake = Body.IsAwake(),
            bullet = Body.IsBullet(),
            enabled = Body.IsEnabled(),
            fixedRotation = Body.IsFixedRotation(),
            gravityScale = Body.GetGravityScale(),
            linearDamping = Body.GetLinearDamping(),
            linearVelocity = Body.GetLinearVelocity(),
            position = GameObject.Transform.Position,
            type = Body.Type(),
            userData = this
        };

        Game.PhysicsWorld.DestroyBody(Body);
        Body = Game.PhysicsWorld.CreateBody(bodyDef);
    }

    public override void FixedUpdate()
    {
        GameObject.Transform.Translate(Position - prevPosition);
        prevPosition = Position;
    }

    public virtual void OnCollisionEnter(Collision collision) => CollisionEnter?.Invoke(collision);

    public virtual void OnCollisionExit(Collision collision) => CollisionExit?.Invoke(collision);

    public virtual void OnTriggerEnter(Collision collision) => TriggerEnter?.Invoke(collision);

    public virtual void OnTriggerExit(Collision collision) => TriggerExit?.Invoke(collision);
}