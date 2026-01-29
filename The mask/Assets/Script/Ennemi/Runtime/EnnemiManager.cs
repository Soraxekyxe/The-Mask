using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class EnnemiManager : MonoBehaviour
{
    private readonly List<Ennemi> ennemis = new();

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
    }
}
