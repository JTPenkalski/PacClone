namespace GameEngine;

public static class Random
{
    private static readonly System.Random random = new();

    public static float Value => (float)random.NextDouble();

    public static float Float(float max) => (float)random.NextDouble() * max;

    public static int Integer(int max) => random.Next(max);

    public static int Range(int min, int max) => random.Next(min, max);
}