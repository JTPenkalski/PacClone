using Box2D.NetStandard.Dynamics.Fixtures;

namespace GameEngine;

public class RaycastHit
{
    public float Fraction { get; private set; }
    public Fixture? Fixture { get; private set; }
    public Vector2 Point { get; private set; }
    public Vector2 Normal { get; private set; }

    public static implicit operator bool(RaycastHit hit) => hit.Fixture != null;

    public override string ToString()
    {
        string fixtureStr = Fixture != null ? ((Rigidbody)Fixture.Body.UserData).GameObject.Name : "None";
        return $"Frac = {Fraction}, Fixture = {fixtureStr}, Point = {Point}, Normal = {Normal}";
    }

    public void Create(Fixture fixture, Vector2 point, Vector2 normal, float fraction)
    {
        Fixture = fixture;
        Point = point;
        Normal = normal;
        Fraction = fraction;
    }

    public void ClearFixture() => Fixture = null;
}