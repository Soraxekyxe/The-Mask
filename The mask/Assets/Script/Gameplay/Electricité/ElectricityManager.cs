using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject electricityCanvas;
    public GameObject loseAlertCanvas;
    public GameObject loseFinalCanvas;

    [Header("UI")]
    public Slider electricitySlider;
    public Button holdButton;
    public Button quitButton;

    [Header("Global Darkness")]
    public Image globalDarkOverlay;     // Image noire plein écran (Canvas global)
    public Image vignetteOverlay;       // Image vignette (bords sombres)

    [Header("Settings")]
    public float maxElectricity = 100f;
    public float drainDuration = 20f;    
    public float rechargeSpeed = 30f;     
    public float darknessMaxAlpha = 0.75f;
    public float vignetteMaxAlpha = 0.6f;

    [Header("Darkness Curve")]
    public AnimationCurve darknessCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Audio")]
    public AudioSource loseSound;

    private float currentElectricity;
    private bool isHolding = false;
    private bool isDead = false;

    void Awake()
    {
        currentElectricity = maxElectricity;

        electricitySlider.maxValue = maxElectricity;
        electricitySlider.value = currentElectricity;

        quitButton.onClick.AddListener(CloseCanvas);

        electricityCanvas.SetActive(false);
        loseAlertCanvas.SetActive(false);
        loseFinalCanvas.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        DrainElectricity();
        UpdateUI();
        CheckLose();
    }
    
    private void DrainElectricity()
    {
        float drainPerSecond = maxElectricity / drainDuration;

        if (!isHolding)
            currentElectricity -= drainPerSecond * Time.deltaTime;
        else
            currentElectricity += rechargeSpeed * Time.deltaTime;

        currentElectricity = Mathf.Clamp(currentElectricity, 0, maxElectricity);
    }
    
    private void UpdateUI()
    {
        electricitySlider.value = currentElectricity;

        float t = 1f - (currentElectricity / maxElectricity);

        // courbe → plus sombre plus vite au début
        float curvedT = darknessCurve.Evaluate(t);

        // overlay global
        if (globalDarkOverlay != null)
        {
            Color bg = globalDarkOverlay.color;
            bg.a = Mathf.Lerp(0f, darknessMaxAlpha, curvedT);
            globalDarkOverlay.color = bg;
        }

        // vignette (bords plus sombres)
        if (vignetteOverlay != null)
        {
            Color v = vignetteOverlay.color;
            v.a = Mathf.Lerp(0f, vignetteMaxAlpha, curvedT);
            vignetteOverlay.color = v;
        }
    }

    private void CheckLose()
    {
        if (currentElectricity <= 0 && !isDead)
            StartCoroutine(LoseSequence());
    }

    // 🔘 bouton maintenu
    public void HoldButtonDown()
    {
        isHolding = true;
    }

    public void HoldButtonUp()
    {
        isHolding = false;
    }

    private IEnumerator LoseSequence()
    {
        isDead = true;

        electricityCanvas.SetActive(false);
        loseAlertCanvas.SetActive(true);

        if (loseSound != null)
            loseSound.Play();

        yield return new WaitForSeconds(2f);

        loseAlertCanvas.SetActive(false);
        loseFinalCanvas.SetActive(true);
    }

    public void OpenCanvas()
    {
        if (isDead) return;
        electricityCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        electricityCanvas.SetActive(false);
        isHolding = false;
    }
}
