using System.Diagnostics;

namespace GameEngine;

public static class Game
{
    public static event Action? FixedUpdate;
    public static event Action? Initialize;
    public static event Action? Update;

    public static long FixedTimestep { get; set; } = 16;
    public static IReadOnlyList<GameObject> GameObjects => (IReadOnlyList<GameObject>)gameObjects;

    private static bool initialized;
    private static long lag;
    private static long currentTime;
    private static long previousTime;
    private static readonly Stopwatch stopwatch = Stopwatch.StartNew();
    private static readonly IList<GameObject> gameObjects = new List<GameObject>();

    public static void Start()
    {
        previousTime = DateTime.Now.Millisecond;
        currentTime = previousTime;

        Application.Idle += IdleTick;
    }

    public static void Stop()
    {
        Application.Idle -= IdleTick;
    }

    public static GameObject Instantiate(GameObject gameObject)
    {
        gameObject.ID = gameObjects.Count;

        gameObjects.Add(gameObject);

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
            FixedUpdate?.Invoke();

            lag -= FixedTimestep;
        }

        Update?.Invoke();
    }
}