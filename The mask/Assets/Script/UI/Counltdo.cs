using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Counltdo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coultdownText;
    [SerializeField] private float remainingTime = 60f;
    [Header("Scene")]
    [SerializeField] private string loseSceneName = "SceneLose";
    private bool hasEnded = false;
    private void Update()
    {
        if (hasEnded) return;

        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0f);
            UpdateText();
        }
        else
        {
            hasEnded = true;
            SceneManager.LoadScene(loseSceneName);
        }
    }
    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        coultdownText.text = $"{minutes:0}:{seconds:00}";
    }
}