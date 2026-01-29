using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ennemi : MonoBehaviour
{
    public MonsterData monster;
    public EnnemiManager ennemiManager;
    public AudioClip clip;
    public AudioSource scream;
    public GameObject ennemi;
    private Coroutine walking;
    int corridor;
    public Player player;


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
            int CanMove = Random.Range(0, 10);
            Debug.Log($"Random = {CanMove}" );
            
            //Regarde si le lancer de dé est bon, si oui alors il bouge//
            if (CanMove <= monster.Walk)
            {
                corridor++;
                Debug.Log($"Corridor = {corridor}");
            }

            if (corridor == 3)
            {
                clip = monster.Sounds[0];
                if (clip != null)
                {
                    scream.clip = clip;
                    scream.volume = 0.05f;
                    
                    scream.Play();
                    
                    Debug.unityLogger.Log("Ennemi cream");
                }
            }
            
            //regarde si le monstre est arrivé dans la salle//
            if (corridor == monster.Distance)
            {
                corridor = 0;
                Debug.Log($"Corridor = {corridor}");
                
                MaskCheck();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
    
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

    void Safe()
    {
        Debug.Log("Safe");

        if (ennemi != null)
            ennemi.SetActive(true);
    }

    //Le Screamer
    void Screamer()
    {
        Debug.Log("Screamer");
        
        //Ajoute le son du monstre data dans le l'AudioClip//
        clip = monster.Sounds[0];
        if (clip != null)
        {
            //Met le son dans l'audio source//
            scream.clip = clip;
            scream.volume = 1f;
            
            scream.Play();
        }

        ennemiManager.Stop();
        
        if (ennemi != null)
            ennemi.SetActive(true);
    }
    
    //Arréte le déplacement du monstre//
    public void StopWalking()
    {
        StopCoroutine(walking);
    }
}
