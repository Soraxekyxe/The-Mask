using System.Collections.Generic;
using UnityEngine;
//là ou on répertorie les mask, le gars qui gère tout
[CreateAssetMenu(fileName = "MaskDatabase", menuName = "Scriptable Objects/Mask Database")]
playerMaskChecker.SetMask(maskData.maskID);
public class MaskDatabase : ScriptableObject
{
    public List<MaskData> masks = new List<MaskData>();

}

