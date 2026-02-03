using System;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskWear : MonoBehaviour, IPointerDownHandler
{
   [Header("Script")]
   public DataMasks masks;
   public Player player;
   
   [Header ("Sprite")]
   public GameObject inFace;
   public GameObject masksManager;
   
   [Header ("Désactiver")]
   public GameObject qteBox;
   public GameObject electricBox;

   public void OnPointerDown(PointerEventData click)
   {
      HoldMask();
   }

   public void HoldMask()
   {
      player.currentMaskID = masks.maskID;
      inFace.SetActive(true);
      masksManager.SetActive(false);
      qteBox.SetActive(false);
      electricBox.SetActive(false);
      
      Debug.Log($"Porte masque = {player.currentMaskID}");
   }
}
