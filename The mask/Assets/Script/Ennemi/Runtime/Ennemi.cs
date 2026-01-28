using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ennemi : MonoBehaviour
{
    public MonsterData monster;
    public AudioClip clip;
    public AudioSource scream;
    public GameObject ennemi;
    int corridor;
    public bool ImHere;

    void Start()
    {
        StartCoroutine(Walking());

        clip = monster.Sounds[0];
        if (clip != null) ;
        
        scream.clip = clip;
    }

    IEnumerator Walking()
    {
        while (true)
        {
            int CanMove = Random.Range(0, 10);
            Debug.Log($"Random = {CanMove}" );

            if (CanMove <= monster.Walk)
            {
                Move();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    void Move()
    {
        corridor++;
        Debug.Log($"Corridor = {corridor}");

        if (corridor == monster.Distance)
        {
            corridor = 0;
            
            Screamer();
            
        }

        else
        {
            Walking();
        }
    }

    void MaskCheck()
    {
           
    }

    void Screamer()
    {
        Debug.Log("Screamer");
        
        ImHere = true;
        
        scream.Play();
        
        StopAllCoroutines();
        
        ennemi.SetActive(true);
    }

    void Awake()
    {
        if (ImHere = true)
        {
            StopAllCoroutines();
        }

        StartCoroutine(Walking());
    }
}
