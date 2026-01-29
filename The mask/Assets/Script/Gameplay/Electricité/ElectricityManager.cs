using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElectricityManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject electricityCanvas;

    [Header("UI")]
    public Slider electricitySlider;
    public Button quitButton;

    [Header("Global Darkness")]
    public Image globalDarkOverlay;
    public Image vignetteOverlay;

    [Header("Settings")]
    public float maxElectricity = 100f;
    public float drainDuration = 20f;
    public float rechargeSpeed = 30f;
    public float darknessMaxAlpha = 0.75f;
    public float vignetteMaxAlpha = 0.6f;

    [Header("Darkness Curve")]
    public AnimationCurve darknessCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Lose / Screamer")]
    public string defeatSceneName = "SceneLose";
    public GameObject screamerOverlay;     // SetActive(false) au départ
    public AudioSource screamerSound;      // Play On Awake = OFF
    public float screamerDuration = 2f;

    private float currentElectricity;
    private bool isHolding = false;
    private bool isDead = false;

    void Awake()
    {
        // sécurité son
        if (screamerSound != null)
        {
            screamerSound.playOnAwake = false;
            screamerSound.Stop();
        }

        currentElectricity = maxElectricity;
        electricitySlider.maxValue = maxElectricity;
        electricitySlider.value = currentElectricity;

        quitButton.onClick.AddListener(CloseCanvas);

        if (electricityCanvas != null) electricityCanvas.SetActive(false);
        if (screamerOverlay != null) screamerOverlay.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        DrainElectricity();
        UpdateUI();

        if (currentElectricity <= 0f)
            StartCoroutine(LoseSequence());
    }

    private void DrainElectricity()
    {
        float drainPerSecond = maxElectricity / drainDuration;

        if (!isHolding) currentElectricity -= drainPerSecond * Time.deltaTime;
        else currentElectricity += rechargeSpeed * Time.deltaTime;

        currentElectricity = Mathf.Clamp(currentElectricity, 0, maxElectricity);
    }

    private void UpdateUI()
    {
        electricitySlider.value = currentElectricity;

        float t = 1f - (currentElectricity / maxElectricity);
        float curvedT = darknessCurve.Evaluate(t);

        if (globalDarkOverlay != null)
        {
            Color bg = globalDarkOverlay.color;
            bg.a = Mathf.Lerp(0f, darknessMaxAlpha, curvedT);
            globalDarkOverlay.color = bg;
        }

        if (vignetteOverlay != null)
        {
            Color v = vignetteOverlay.color;
            v.a = Mathf.Lerp(0f, vignetteMaxAlpha, curvedT);
            vignetteOverlay.color = v;
        }
    }

    public void HoldButtonDown() => isHolding = true;
    public void HoldButtonUp() => isHolding = false;

    public void OpenCanvas()
    {
        if (isDead) return;
        if (electricityCanvas != null) electricityCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        if (electricityCanvas != null) electricityCanvas.SetActive(false);
        isHolding = false;
    }

    private IEnumerator LoseSequence()
    {
        if (isDead) yield break;
        isDead = true;

        // ferme le canvas électricité
        if (electricityCanvas != null) electricityCanvas.SetActive(false);

        // screamer visuel + son
        if (screamerOverlay != null) screamerOverlay.SetActive(true);
        if (screamerSound != null) screamerSound.Play();

        yield return new WaitForSeconds(screamerDuration);

        // charge la scène défaite
        LevelSession.Stop();
        SceneManager.LoadScene(defeatSceneName);
    }
}
