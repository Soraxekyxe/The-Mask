using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    [Header("Script")]
    private readonly List<Ennemi> ennemis = new();
    private readonly List<MaskRemove> removesMask = new();
    private readonly List<CarréClickable> clickablesCarré = new();
    public QTEManager qteManager;
    public ElectricityManager electricityManager;

    [Header("Désactiver")] 
    public GameObject qteBox;
    public GameObject electricBox;
    
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
        
        MaskRemove[] search = FindObjectsOfType<MaskRemove>(true);
        {
            foreach (MaskRemove remove in search)
            {
                if(remove != null && !removesMask.Contains(remove))
                    removesMask.Add(remove);
            }
        }
    }

    //Arréte touts les ennemis de la scéne//
    public void Stop()
    {
        foreach (Ennemi ennemi in ennemis)
        {
            ennemi.StopWalking();
        }
        
        foreach (CarréClickable Square in clickablesCarré)
        {
            if (Square != null)
                Square.enabled = false;
        }
        
        mouseClick.enabled = false;
        electricityManager.PauseElectricity();
        Debug.Log("Souris ne marche plus");

        WaitMonsterLeave = StartCoroutine(WaitMonster());
    }
    
    //Sa permet d'attendre quelque seconde avant que le monstre part (cinématique qui désactive les commandes)//
    IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(5);
         mouseClick.enabled = true;

         foreach (Ennemi ennemi in ennemis)
         {
             if (ennemi != null && ennemi.ennemi != null && ennemi.ennemi.activeSelf)
             {
                 ennemi.ennemi.SetActive(false);
                 
                 Debug.Log("Je part");
             }
             
             ennemi.ResumeWalking();
         }
         electricityManager.ResumeElectricity();
         
         Debug.Log("Remarché SVP");
    }

    //Lance la cinématique du screamer//
    public void HeScream()
    {
        qteManager.CloseQTE();
        electricityManager.CloseCanvas();
        
        qteBox.SetActive(false);
        electricBox.SetActive(false);
        
        foreach (MaskRemove removes in removesMask)
        {
            if(removes != null)
            if (removes.inFace.activeSelf)
            {
                removes.UnHoldMask();
                
                qteBox.SetActive(false);
                electricBox.SetActive(false);
                 
                Debug.Log("Monstre enléve le masque");
            }
        }
        
        foreach (Ennemi ennemi in ennemis)
        {
            ennemi.StopWalking();
        }
        mouseClick.enabled = false;
        electricityManager.PauseElectricity();
        WaitScreamer = StartCoroutine(waitScreamer());

    }

    //Cinématique du screamer et lance le game over//
    IEnumerator waitScreamer()
    {
        yield return new WaitForSeconds(15);
        
        GameOver();
    }
    
    //Le game over//
    public void GameOver()
    {
        SceneManager.LoadScene(defeatSceneName);
        Debug.Log("Game Over");
    }
}
