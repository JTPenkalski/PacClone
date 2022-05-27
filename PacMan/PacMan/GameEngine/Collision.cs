using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;

namespace GameEngine;

public class Collision : ContactListener
{
    public override void BeginContact(in Contact contact)
    {
        if (contact.FixtureA.Body.UserData is Rigidbody rigidbodyA && contact.FixtureB.Body.UserData is Rigidbody rigidbodyB)
        {
            if (rigidbodyB.Collider != null)
                rigidbodyA.OnCollisionEnter(rigidbodyB.Collider);

            if (rigidbodyA.Collider != null)
                rigidbodyB.OnCollisionEnter(rigidbodyA.Collider);
        }
    }

    public override void EndContact(in Contact contact)
    {
        if (contact.FixtureA.UserData is Rigidbody rigidbodyA && contact.FixtureB.UserData is Rigidbody rigidbodyB)
        {
            if (rigidbodyB.Collider != null)
                rigidbodyA.OnCollisionExit(rigidbodyB.Collider);

            if (rigidbodyA.Collider != null)
                rigidbodyB.OnCollisionExit(rigidbodyA.Collider);
        }
    }

    public override void PreSolve(in Contact contact, in Manifold oldManifold) { }

    public override void PostSolve(in Contact contact, in ContactImpulse impulse) { }
}