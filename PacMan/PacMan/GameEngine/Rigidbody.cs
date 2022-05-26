using Box2D.NetStandard.Dynamics.Bodies;

namespace GameEngine;

public class Rigidbody : Component
{
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
        if (Game.PhysicsWorld == null)
            throw new InvalidOperationException("Cannot add a Rigidbody component without a Physics World initialized in the Game.");

        // Create a temporary Body property for the client to configure
        Body = Game.PhysicsWorld.CreateBody(new BodyDef());
    }

    public override void Initialize()
    {
        if (Game.PhysicsWorld == null)
            throw new InvalidOperationException("Cannot add a Rigidbody component without a Physics World initialized in the Game.");

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
            userData = Body.UserData
        };

        Game.PhysicsWorld.DestroyBody(Body);
        Body = Game.PhysicsWorld.CreateBody(bodyDef);
    }

    public override void FixedUpdate()
    {
        GameObject.Transform.Translate(Position - prevPosition);
        prevPosition = Position;
    }
}