using Box2D.NetStandard.Dynamics.Fixtures;

namespace GameEngine;

public static class Physics
{
    public static RaycastHit RayCast(Vector2 origin, Vector2 direction, float maxDistance = 1f, Fixture[]? ignoredFixtures = null)
    {
        RaycastHit hit = new();
        Game.PhysicsWorld.RayCast(hit.Create, origin, origin + (maxDistance * direction));

        if (hit.Fixture != null && hit.Fixture.IsSensor())
        {
            hit.ClearFixture();
        }
        else if (ignoredFixtures != null)
        {
            foreach (Fixture f in ignoredFixtures)
            {
                if (f == hit.Fixture)
                {
                    hit.ClearFixture();
                    break;
                }
            }
        }

        return hit;
    }
}