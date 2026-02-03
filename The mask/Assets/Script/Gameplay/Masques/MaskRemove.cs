using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskRemove : MonoBehaviour, IPointerDownHandler
{
    [Header("Script")]
    public Player player;
    
    [Header("Sprite")]
    public GameObject inFace;
    public GameObject masksManager;
    
    [Header ("Désactiver")]
    public GameObject qteBox;
    public GameObject electricBox;

    public void OnPointerDown(PointerEventData click)
    {
        UnHoldMask();
    }

    public void UnHoldMask()
    {
        player.currentMaskID = -1;
        inFace.SetActive(false);
        masksManager.SetActive(true);
        qteBox.SetActive(true);
        electricBox.SetActive(true);
      
        Debug.Log("masque retiré");
    }
}
