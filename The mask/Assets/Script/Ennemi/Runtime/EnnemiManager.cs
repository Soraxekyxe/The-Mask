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

    [Header("Cinématic")] 
    public IronMaidenCinematic ironMaidenCinematic;

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
    private Coroutine WalkMonsters;

    [Header("Variable Walking")] 
    public int corridor;
    public int whoWalk;
    
    [Header("Variable Sounds")]
    public int firtsSound;
    public int secondSound;
    public int thirdSound;
    
   [Header("Variable Random for who walk")] 
    public int randomMax;
    public int randomMin;

    public bool stopRandom;
    
    public AudioClip closeDoor;
    public AudioSource audio;

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

        WalkMonsters = StartCoroutine(WalkInCorridor());
    }

    IEnumerator WalkInCorridor()
    {
        while(true)
        {
            if (stopRandom == false)
            {
                whoWalk = Random.Range(randomMin, randomMax);
                Debug.Log($"WhoWalk = {whoWalk}" );
            }
            
            foreach (Ennemi ennemi in ennemis)
            {
                if (whoWalk >= ennemi.monster.minWalk && whoWalk <= ennemi.monster.maxWalk)
                {
                    ennemi.monster.Distance++;
                    Debug.Log($"Ennemi distance = {ennemi.monster.Distance}");
                }

                if (ennemi.monster.Distance == firtsSound)
                {
                    ennemi.clip = ennemi.monster.Sounds[Random.Range(0, ennemi.monster.Sounds.Length)];
                    if (ennemi.clip != null)
                    {
                        ennemi.scream.clip = ennemi.clip;
                        ennemi.scream.volume = 0.15f;
                    
                        ennemi.scream.Play();
                        
                        Debug.Log("Ennemi firts sound");
                    }
                    
                    stopRandom = true;
                    Debug.Log($"stopRandom = {stopRandom}");
                    
                    yield return new WaitForSeconds(3f);
                }
                
                stopRandom = false;

                if (ennemi.monster.Distance == secondSound)
                {
                    ennemi.clip = ennemi.monster.Sounds[Random.Range(0, ennemi.monster.Sounds.Length)];
                    if (ennemi.clip != null)
                    {
                        ennemi.scream.clip = ennemi.clip;
                        ennemi.scream.volume = 0.15f;
                    
                        ennemi.scream.Play();
                    
                        Debug.Log("Ennemi second sound");
                    }
                    
                    stopRandom = true;
                    Debug.Log($"stopRandom = {stopRandom}");
                    
                    yield return new WaitForSeconds(3f);
                }
                
                stopRandom = false;

                if (ennemi.monster.Distance == thirdSound)
                {
                    ennemi.clip = ennemi.monster.Sounds[Random.Range(0, ennemi.monster.Sounds.Length)];
                    if (ennemi.clip != null)
                    {
                        ennemi.scream.clip = ennemi.clip;
                        ennemi.scream.volume = 0.15f;
                    
                        ennemi.scream.Play();
                    
                        Debug.Log("Ennemi third sound");
                    }
                    
                    stopRandom = true;
                    Debug.Log($"stopRandom = {stopRandom}");
                    
                    yield return new WaitForSeconds(3f);
                }
                
                stopRandom = false;

                if (ennemi.monster.Distance == corridor)
                {
                    ennemi.monster.Distance = 0;
                    Debug.Log($"[{ennemi.monster.name}] =" + ennemi.monster.Distance);
                    
                    StopCoroutine(WalkInCorridor());
                    
                    ennemi.MaskCheck();
                }
            }
            yield return new WaitForSeconds(2);
        }
    }

    //Arréte touts les ennemis de la scéne//
    public void Stop()
    {
        foreach (Ennemi ennemi in ennemis)
        {
            ennemi.StopWalking();
        }
        
        qteBox.SetActive(false);
        electricBox.SetActive(false);
        
        electricityManager.PauseElectricity();
        mouseClick.enabled = false;
        electricityManager.PauseElectricity();
        Debug.Log("Souris ne marche plus");
        
        WaitMonsterLeave = StartCoroutine(WaitMonster());
    }
    
    //Sa permet d'attendre quelque seconde avant que le monstre part (cinématique qui désactive les commandes)//
    IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(10);
         mouseClick.enabled = true;

         WalkMonsters = StartCoroutine(WalkInCorridor());
         
         foreach (MaskRemove removes in removesMask)
         {
             if(removes != null)
                 if(removes.inFace.activeSelf)
                 {
                     removes.UnHoldMask();
                
                     qteBox.SetActive(true);
                     electricBox.SetActive(true);
                 
                     Debug.Log("Monstre enléve le masque");
                 }
         }
         
         if (closeDoor != null)
         {
             audio.clip = closeDoor;
             audio.Play();
         }
         
         electricityManager.ResumeElectricity();
         
         electricityManager.ResumeElectricity();
         Debug.Log("Remarché SVP");
         
         
    }

    //Lance la cinématique du screamer//
    public void HeScream()
    {
        Debug.Log("Tu te lance ?");
        
        qteManager.CloseQTE();
        electricityManager.CloseCanvas();
        
        qteBox.SetActive(false);
        electricBox.SetActive(false);
        electricityManager.PauseElectricity();
        
        foreach (MaskRemove removes in removesMask)
        {
            if(removes != null)
            if(removes.inFace.activeSelf)
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
        Debug.Log("Tu bug ?");
        yield return new WaitForSeconds(3);
        
        GameOver();
    }
    
    //Le game over//
    public void GameOver()
    {
        SceneManager.LoadScene(defeatSceneName);
        Debug.Log("Game Over");
    }
}
