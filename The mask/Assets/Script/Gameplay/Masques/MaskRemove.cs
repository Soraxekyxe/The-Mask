using System;
using UnityEngine;

public class MaskRemove : MonoBehaviour
{
    public Player player;
    public GameObject inFace;
    public GameObject masksManager;

    private void OnMouseDown()
    {
        UnHoldMask();
    }

    public void UnHoldMask()
    {
        player.currentMaskID = -1;
        inFace.SetActive(false);
        masksManager.SetActive(true);
      
        Debug.Log("masque retiré");
    }
}
