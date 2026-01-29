using UnityEngine;

public static class LevelSession
{
    private static float startTime;
    private static bool running;

    public static string LevelSceneName { get; private set; }
    public static float ElapsedSeconds { get; private set; }

    public static void Begin(string levelSceneName)
    {
        LevelSceneName = levelSceneName;
        startTime = Time.time;
        ElapsedSeconds = 0f;
        running = true;
    }

    public static void Stop()
    {
        if (!running) return;
        ElapsedSeconds = Time.time - startTime;
        running = false;
    }

    public static string FormatTime(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        return $"{m:0}:{s:00}";
    }
}
