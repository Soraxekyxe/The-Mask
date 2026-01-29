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
    public PlayerMaskChecker player;

    void Start()
    {
        walking = StartCoroutine(StartWalking());

        //Ajoute le son du monstre data dans le l'AudioClip//
        clip = monster.Sounds[0];
        if (clip != null)
            
        //Met le son dans l'audio source//
        scream.clip = clip;
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
            
            //regarde si le monstre est arrivé dans la salle//
            if (corridor == monster.Distance)
            {
                corridor = 0;
                Debug.Log($"Corridor = {corridor}");
                
                Screamer();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
    
    void MaskCheck()
    {
           
    }

    //Le Screamer
    void Screamer()
    {
        Debug.Log("Screamer");
        
        scream.Play();
        
        ennemiManager.Stop();
        
        ennemi.SetActive(true);
    }
    
    //Arréte le déplacement du monstre//
    public void StopWalking()
    {
        StopCoroutine(walking);
    }
}
