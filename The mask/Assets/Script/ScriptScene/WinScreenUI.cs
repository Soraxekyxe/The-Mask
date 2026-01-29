using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreenUI : MonoBehaviour
{
    public TMP_Text timeText;
    public string menuSceneName = "Starting";

    void Start()
    {
        if (timeText != null)
            timeText.text = LevelSession.FormatTime(LevelSession.ElapsedSeconds);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(LevelSession.LevelSceneName);
    }
}