using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Audio")]
    public AudioSource pauseMusic;
    private AudioSource[] otherMusic; 
    private bool paused = false;

    void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIState.IsAnyPopupOpen = false;
            if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
        otherMusic = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in otherMusic)
        {
            if (audio != pauseMusic)
                audio.Pause(); 
        }
        if (pauseMusic != null)
            pauseMusic.Play(); 
    }

    public void Resume()
    {
        paused = false;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
        
        if (otherMusic != null)
        {
            foreach (AudioSource audio in otherMusic)
            {
                if (audio != pauseMusic)
                    audio.UnPause();
            }
        }
        
        if (pauseMusic != null)
            pauseMusic.Stop();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnExitClick()
    {
#if UNITY_EDITOR
        Debug.Log("okazou pr vr si c oké");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

