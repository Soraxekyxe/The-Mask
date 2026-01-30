using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    [Header("Ennemi")]
    private readonly List<Ennemi> ennemis = new();
    
    [Header("Commande")]
    public InputSystemUIInputModule mouseClick;
    
    [Header("Scéne")]
    public string defeatSceneName = "SceneLose";
    
    [Header("Coroutine")]
    private Coroutine WaitMonsterLeave;
    private Coroutine WaitScreamer;

    void Start()
    {
        //Récupére touts les ennemis de la scéne
        Ennemi[] found = FindObjectsOfType<Ennemi>(includeInactive: true);
        foreach (Ennemi ennemi in found)
        {
            if(ennemi != null && !ennemis.Contains(ennemi))
            ennemis.Add(ennemi);
        }
    }

    //Arréte touts les ennemis de la scéne//
    public void Stop()
    {
        foreach (Ennemi ennemi in ennemis)
        {
            ennemi.StopWalking();
        }
        mouseClick.enabled = false;
        Debug.Log("Souris ne marche plus");

        WaitMonsterLeave = StartCoroutine(WaitMonster());
    }

    IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(2);
        
        mouseClick.enabled = true;
    }

    public void HeScream()
    {
        mouseClick.enabled = false;
        WaitScreamer = StartCoroutine(waitScreamer());

    }

    IEnumerator waitScreamer()
    {
        yield return new WaitForSeconds(15);
        
        GameOver();
    }
    public void GameOver()
    {
        SceneManager.LoadScene(defeatSceneName);
        Debug.Log("Game Over");
    }
}
