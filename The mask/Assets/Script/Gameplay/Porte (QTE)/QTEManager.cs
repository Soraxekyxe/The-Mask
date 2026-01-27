using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEManager : MonoBehaviour
{
    public enum Dir { Up, Down, Left, Right }

    [Header("Progress")]
    public bool keepProgressWhenClosed = true;
    
    [Header("Canvases")]
    public GameObject qteCanvas;
    public GameObject winCanvas;

    [Header("UI")]
    public TMP_Text stepText;          // "1/10"
    public Transform arrowsContainer;  // parent des flèches
    public QTEArrowUI arrowPrefab;     // prefab (Image + QTEArrowUI)
    public Button closeButton;

    [Header("Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    [Header("Settings")]
    public int totalSteps = 10;
    public int baseArrowsStep1 = 3; // étape 1 = 3 flèches
    public int addPerStep = 1;      // +1 par étape
    public float errorDelay = 1f;   // temps pour voir le rouge

    private int currentStep = 1;
    private readonly List<Dir> sequence = new List<Dir>();
    private readonly List<QTEArrowUI> spawnedUI = new List<QTEArrowUI>();
    private int index = 0;

    private bool isRunning = false;
    private bool isWaiting = false;

    void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseQTE);

        if (qteCanvas != null) qteCanvas.SetActive(false);
        if (winCanvas != null) winCanvas.SetActive(false);
    }

    void Update()
    {
        if (!isRunning || isWaiting) return;

        if (TryGetDirectionInput(out Dir inputDir))
            CheckInput(inputDir);
    }

    public void StartQTE()
    {
        StopAllCoroutines();
        isWaiting = false;

        if (winCanvas != null) winCanvas.SetActive(false);

        OpenQTE();
        
        if (!keepProgressWhenClosed)
            currentStep = 1;
        
        currentStep = Mathf.Clamp(currentStep, 1, totalSteps);
        
        StartStep(newSequence: true);
    }


    private void OpenQTE()
    {
        if (qteCanvas != null) qteCanvas.SetActive(true);
        isRunning = true;
    }

    public void CloseQTE()
    {
        StopAllCoroutines();
        isRunning = false;
        isWaiting = false;

        ClearArrows();

        if (qteCanvas != null) qteCanvas.SetActive(false);
    }


    private void StartStep(bool newSequence)
    {
        index = 0;

        ClearArrows();

        if (newSequence)
            GenerateSequenceForCurrentStep();

        DrawSequenceUI();

        if (stepText != null)
            stepText.text = $"{currentStep}/{totalSteps}";
    }

    private void GenerateSequenceForCurrentStep()
    {
        sequence.Clear();

        int count = baseArrowsStep1 + (currentStep - 1) * addPerStep;

        for (int i = 0; i < count; i++)
        {
            Dir d = (Dir)Random.Range(0, 4);
            sequence.Add(d);
        }
    }

    private void DrawSequenceUI()
    {
        spawnedUI.Clear();

        for (int i = 0; i < sequence.Count; i++)
        {
            var ui = Instantiate(arrowPrefab, arrowsContainer);
            ui.SetSprite(GetSprite(sequence[i]));
            ui.SetNeutral();
            spawnedUI.Add(ui);
        }
    }

    private Sprite GetSprite(Dir d)
    {
        switch (d)
        {
            case Dir.Up: return upSprite;
            case Dir.Down: return downSprite;
            case Dir.Left: return leftSprite;
            case Dir.Right: return rightSprite;
            default: return upSprite;
        }
    }

    private void CheckInput(Dir inputDir)
    {
        if (index < 0 || index >= sequence.Count) return;

        Dir expected = sequence[index];

        if (inputDir == expected)
        {
            spawnedUI[index].SetCorrect();
            index++;

            if (index >= sequence.Count)
            {
                currentStep++;

                if (currentStep > totalSteps)
                {
                    Win();
                }
                else
                {
                    // étape suivante => nouvelle séquence
                    StartStep(newSequence: true);
                }
            }
        }
        else
        {
            // erreur => rouge visible + restart même étape (même séquence)
            spawnedUI[index].SetWrong();
            StartCoroutine(RestartCurrentStepAfterDelay());
        }
    }

    private IEnumerator RestartCurrentStepAfterDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(errorDelay);
        isWaiting = false;

        // Restart étape => on garde la même séquence (comme “recommencer l’étape”)
        StartStep(newSequence: false);
    }

    private void Win()
    {
        StopAllCoroutines();
        isRunning = false;
        isWaiting = false;

        if (qteCanvas != null) qteCanvas.SetActive(false);
        if (winCanvas != null) winCanvas.SetActive(true);
    }

    private void ClearArrows()
    {
        if (arrowsContainer == null) return;

        for (int i = arrowsContainer.childCount - 1; i >= 0; i--)
            Destroy(arrowsContainer.GetChild(i).gameObject);

        spawnedUI.Clear();
    }

    private bool TryGetDirectionInput(out Dir dir)
    {
        // Haut : Z (AZERTY) ou W (QWERTY) ou ↑
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir = Dir.Up; return true;
        }

        // Bas : S ou ↓
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir = Dir.Down; return true;
        }

        // Gauche : Q (AZERTY) ou A (QWERTY) ou ←
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = Dir.Left; return true;
        }

        // Droite : D ou →
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = Dir.Right; return true;
        }

        dir = Dir.Up;
        return false;
    }
}
