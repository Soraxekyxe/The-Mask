using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Ennemi : MonoBehaviour
{
    [Header("Script")]
    public MonsterData monster;
    public EnnemiManager ennemiManager;
    public Player player;
    public CinematicManager cinematicManager;
    
    [Header("Sprite")]
    public GameObject ennemi;
    
    [Header("Audio")]
    public AudioClip clip;
    public AudioSource scream;
    public AudioClip door;
    
    private Coroutine walking;
    int corridor;
    
    //Vérifie si le joueur a le bon masque//
    public void MaskCheck()
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
        cinematicManager.PlayCinematic();
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
        
        if (walking != null)
        {
            StopCoroutine(walking);
            walking = null;
            Debug.Log("Stop walking");
        }
        
    }
}
