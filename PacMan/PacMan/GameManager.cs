namespace PacMan;

public static class GameManager
{
    public static event Action<string>? LevelChanged;

    public const string LEVELS_PATH = $@"{Program.PROJECT_PATH}\Mazes\Levels.txt";

    public static int Level { get; private set; }

    private static readonly string[] levels;

    static GameManager()
    {
        levels = File.ReadAllText(LEVELS_PATH).Split(',');
    }

    public static void MoveNextLevel()
    {
        Level = Math.Max(Level + 1, 0);
        LevelChanged?.Invoke(levels[Level - 1]);
    }

    public static void MovePrevLevel()
    {
        Level = Math.Min(Level - 1, levels.Length);
        LevelChanged?.Invoke(levels[Level - 1]);
    }
}