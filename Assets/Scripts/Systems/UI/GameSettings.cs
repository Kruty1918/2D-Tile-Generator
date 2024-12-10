using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public static class GameSettings
{
    private static bool isCloseSwipe = false;
    private static WorldData _world;

    public static WorldData World { get { return _world;} set { _world = value; } }
    public static bool IsCloseSwipe { get { return isCloseSwipe; } }

    public const string LoadSceneName = "Load";

    // True is close window by swipe and false close all window by button
    public static void SetCloseSwipe(bool active)
    {
        isCloseSwipe = active;
    }
}

public static class SceneLoader
{
    public static string NextSceneName { get; private set; }

    public static void LoadScene(string sceneName)
    {
        NextSceneName = sceneName;
        SceneManager.LoadScene(GameSettings.LoadSceneName);       
    }
}