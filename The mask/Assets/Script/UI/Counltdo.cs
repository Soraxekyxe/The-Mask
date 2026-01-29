using TMPro;
using UnityEngine;

public class Counltdo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coultdownText;
    [SerializeField] private float remainingTime;

    [SerializeField] private GameObject endCanvas;

    [Header("Audio")]
    [SerializeField] private AudioSource lobbyMusic;
    [SerializeField] private AudioSource endMusic; 

    private bool hasEnded = false;

    private void Start()
    {
        endCanvas.SetActive(false);
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (!hasEnded)
        {
            hasEnded = true;
            remainingTime = 0;
            
            if (lobbyMusic != null)
                lobbyMusic.Stop();
            
            if (endMusic != null)
                endMusic.Play();

            endCanvas.SetActive(true);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        coultdownText.text = $"{minutes:0}:{seconds:00}";
    }
}