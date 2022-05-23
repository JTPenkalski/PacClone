namespace GameEngine;

public static class Time
{
    public static long DeltaTime { get; set; }
    public static long FixedDeltaTime => Game.FixedTimestep;
    public static long GameTime { get; set; }
}