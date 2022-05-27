global using Debug = System.Diagnostics.Debug;
global using PacMan.Properties;

namespace PacMan;

/// <summary>
/// Class containing the required bootstrapping code.
/// </summary>
public static class Program
{
    public const string PROJECT_PATH = @"D:\My Projects\C# Projects\PacMan\PacMan\PacMan\bin\Debug\net6.0-windows";

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();

        GameEngine.Game.Start();

        Application.Run(new MainForm());
    }
}