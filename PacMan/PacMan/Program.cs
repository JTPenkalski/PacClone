global using Debug = System.Diagnostics.Debug;
global using PacMan.Properties;

namespace PacMan;

/// <summary>
/// Class containing the required bootstrapping code.
/// </summary>
public static class Program
{
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