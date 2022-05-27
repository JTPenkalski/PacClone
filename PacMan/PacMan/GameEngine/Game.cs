global using System.Numerics;
using Box2D.NetStandard.Dynamics.World;
using System.Diagnostics;

namespace GameEngine;

public static class Game
{
    public static event Action? FixedUpdate;
    public static event Action? Initialize;
    public static event Action? Update;

    public static int PositionIterations { get; set; } = 3;
    public static int VelocityIterations { get; set; } = 8;
    public static long FixedTimestep { get; set; } = 16;
    public static IReadOnlyCollection<GameObject> GameObjects => (IReadOnlyList<GameObject>)gameObjects.Values;
    public static World PhysicsWorld { get; private set; } = new World(new Vector2(0, 10f));

    private static bool initialized;
    private static int id;
    private static long lag;
    private static long currentTime;
    private static long previousTime;
    private static readonly Stopwatch stopwatch = new();
    private static readonly IDictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>();

    static Game()
    {
        PhysicsWorld.SetContactListener(new ContactNotifier());
    }

    public static void Start()
    {
        previousTime = DateTime.Now.Millisecond;
        currentTime = previousTime;

        stopwatch.Start();

        Application.Idle += IdleTick;
    }

    public static void Stop()
    {
        stopwatch.Stop();

        Application.Idle -= IdleTick;
    }

    public static void Destroy(GameObject gameObject)
    {
        gameObjects.Remove(gameObject.ID);
    }

    public static GameObject Instantiate(GameObject gameObject)
    {
        gameObject.ID = id++;

        gameObjects.Add(gameObject.ID, gameObject);

        return gameObject;
    }

    private static void IdleTick(object? sender, EventArgs e)
    {
        while (!NativeMessageHandler.PeekMessage(out _, IntPtr.Zero, 0, 0, 0))
            Tick();
    }

    private static void Tick()
    {
        if (!initialized)
        {
            initialized = true;
            Initialize?.Invoke();
        }

        currentTime = stopwatch.ElapsedMilliseconds;
        Time.DeltaTime = currentTime - previousTime;
        previousTime = currentTime;

        lag += Time.DeltaTime;
        Time.GameTime += Time.DeltaTime;

        while (lag >= FixedTimestep)
        {
            PhysicsWorld?.Step(FixedTimestep, VelocityIterations, PositionIterations);
            FixedUpdate?.Invoke();

            lag -= FixedTimestep;
        }

        Update?.Invoke();
    }
}