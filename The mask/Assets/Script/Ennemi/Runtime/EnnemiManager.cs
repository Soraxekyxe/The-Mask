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
        
        MaskRemove[] mask = FindObjectsOfType<MaskRemove>(true);
        foreach (MaskRemove remove in mask)
        {
            if(remove != null && !removesMask.Contains(remove))
                removesMask.Add(remove);
        }
        
        CarréClickable[] carré = FindObjectsOfType<CarréClickable>(true);
        foreach (CarréClickable Square in carré)
        {
            if (carré != null && !clickablesCarré.Contains(Square))
                clickablesCarré.Add(Square);
            
            Debug.Log("square: " + Square);
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
         
         Debug.Log("Remarché SVP");
    }

    //Lance la cinématique du screamer//
    public void HeScream()
    {
        qteManager.CloseQTE();
        electricityManager.CloseCanvas();

        foreach (CarréClickable Square in clickablesCarré)
        {
            if (Square != null)
                Square.enabled = false;
        }
        
        foreach (MaskRemove removes in removesMask)
        {
            if(removes != null)
            if (removes.inFace.activeSelf)
            {
                removes.UnHoldMask();
                 
                Debug.Log("Monstre enléve le masque");
            }
        }
        
        foreach (Ennemi ennemi in ennemis)
        {
            ennemi.StopWalking();
        }
        mouseClick.enabled = false;
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
