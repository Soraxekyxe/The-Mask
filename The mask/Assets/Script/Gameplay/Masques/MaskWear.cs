using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class MaskWear : MonoBehaviour
{
   public DataMasks masks;
   public Player player;
   public GameObject inFace;
   public GameObject masksManager;

   private void OnMouseDown()
   {
      HoldMask();
   }

   public void HoldMask()
   {
      player.currentMaskID = masks.maskID;
      inFace.SetActive(true);
      masksManager.SetActive(false);
      
      Debug.Log($"Porte masque = {player.currentMaskID}");
   }
}
