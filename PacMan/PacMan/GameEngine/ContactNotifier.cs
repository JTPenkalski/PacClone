using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;

namespace GameEngine;

public class ContactNotifier : ContactListener
{
    public override void BeginContact(in Contact contact)
    {
        if (contact.FixtureA.Body.UserData is Rigidbody rigidbodyA && contact.FixtureB.Body.UserData is Rigidbody rigidbodyB)
        {
            if (rigidbodyB.Collider != null)
            {
                if (contact.FixtureB.IsSensor())
                    rigidbodyA.OnTriggerEnter(new Collision(rigidbodyB.Collider, contact));
                else
                    rigidbodyA.OnCollisionEnter(new Collision(rigidbodyB.Collider, contact));
            }

            if (rigidbodyA.Collider != null)
            {
                if (contact.FixtureA.IsSensor())
                    rigidbodyB.OnTriggerEnter(new Collision(rigidbodyA.Collider, contact));
                else
                    rigidbodyB.OnCollisionEnter(new Collision(rigidbodyA.Collider, contact));
            }
        }
    }

    public override void EndContact(in Contact contact)
    {
        if (contact.FixtureA.UserData is Rigidbody rigidbodyA && contact.FixtureB.UserData is Rigidbody rigidbodyB)
        {
            if (rigidbodyB.Collider != null)
            {
                if (contact.FixtureB.IsSensor())
                    rigidbodyA.OnTriggerExit(new Collision(rigidbodyB.Collider, contact));
                else
                    rigidbodyA.OnCollisionExit(new Collision(rigidbodyB.Collider, contact));
            }

            if (rigidbodyA.Collider != null)
            {
                if (contact.FixtureA.IsSensor())
                    rigidbodyB.OnTriggerExit(new Collision(rigidbodyA.Collider, contact));
                else
                    rigidbodyB.OnCollisionExit(new Collision(rigidbodyA.Collider, contact));
            }
        }
    }

    public override void PreSolve(in Contact contact, in Manifold oldManifold) { }

    public override void PostSolve(in Contact contact, in ContactImpulse impulse) { }
}