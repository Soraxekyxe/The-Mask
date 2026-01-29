using UnityEngine;

public class MaskHolder : MonoBehaviour
{
    public MaskData currentMask;
    public PlayerMaskChecker checker;

    public void SetMask(MaskData mask)
    {
        currentMask = mask;
        checker.SetMask(mask.maskID);

        Debug.Log("Masque actif : " + mask.maskID);
    }
}