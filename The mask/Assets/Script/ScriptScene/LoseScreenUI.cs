using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenUI : MonoBehaviour
{
    public string menuSceneName = "Menu";

    public void GoMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(LevelSession.LevelSceneName);
    }
}