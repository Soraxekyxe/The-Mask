using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Ennemi : MonoBehaviour
{
    [Header("Script")]
    public MonsterData monster;
    public EnnemiManager ennemiManager;
    public Player player;
    
    [Header("Sprite")]
    public GameObject ennemi;
    
    [Header("Audio")]
    public AudioClip clip;
    public AudioSource scream;

    [Header("Son")] 
    public int firts;
    public int seconds;
    
    [Header("Random variable move")]
    public int randomMax;
    public int randomMin;
    
    private Coroutine walking;
    int corridor;
    


    void Start()
    {
        walking = StartCoroutine(StartWalking());
    }

    //Le code qui gére le déplacement du monstre
    IEnumerator StartWalking()
    {
        while (true)
        {
            //Lance un dé pour savoir si le monstre peut bouger//
            int CanMove = Random.Range(randomMin, randomMax);
            Debug.Log($"Random = {CanMove}" );
            
            //Regarde si le lancer de dé est bon, si oui alors il bouge//
            if (CanMove <= monster.Walk)
            {
                corridor++;
                Debug.Log($"Corridor = {corridor}");
            }
            
            //Le monstre est loin
            if (corridor == firts)
            {
                clip = monster.Sounds[Random.Range(0, monster.Sounds.Length)];
                if (clip != null)
                {
                    scream.clip = clip;
                    scream.volume = 0.05f;
                    
                    scream.Play();
                    
                    Debug.Log("Ennemi Scream");
                }
            }
            
            //Le monstre est proche
            if (corridor == seconds)
            {
                clip = monster.laugh;
                if (clip != null)
                {
                    scream.clip = clip;
                    scream.volume = 0.15f;
                    
                    scream.Play();
                    
                    Debug.Log("Ennemi et proche");
                }
            }
            
            //regarde si le monstre est arrivé dans la salle//
            if (corridor == monster.Distance)
            {
                corridor = 0;
                Debug.Log($"Corridor = {corridor}");
                
                MaskCheck();
            }
            
            yield return new WaitForSeconds(2f);
        }
    }
    
    //Vérifie si le joueur a le bon masque//
    void MaskCheck()
    {
        if (player.currentMaskID == monster.monsterID)
        {
            Safe();
        }

        else
        {
            Screamer();
        }
        
    }
    
    //Lorsque le joueur porte le bon masque//
    void Safe()
    {
        Debug.Log("Safe");

        if (ennemi != null)
            ennemi.SetActive(true);
        
        ennemiManager.Stop();
        
        Debug.Log("Safe");
    }

    //Le Screamer//
    void Screamer()
    {
        Debug.Log("Screamer");
        
        //Ajoute le son du monstre data dans le l'AudioClip//
        clip = monster.Scream;
        if (clip != null)
        {
            //Met le son dans l'audio source//
            scream.clip = clip;
            scream.volume = 1f;
            
            scream.Play();
        }
        
        if (ennemi != null)
            ennemi.SetActive(true);
        
        ennemiManager.HeScream();
    }
    
    //Arréte le déplacement du monstre//
    public void StopWalking()
    {
        StopCoroutine(walking);
    }
    
    //Relance la marche//
    public void ResumeWalking()
    {
        walking = StartCoroutine(StartWalking());
    }
}
