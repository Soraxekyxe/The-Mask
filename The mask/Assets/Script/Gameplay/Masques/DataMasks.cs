using System.Collections.Generic;
using UnityEngine;
//là ou on répertorie les mask, le gars qui gère tout

[CreateAssetMenu(fileName = "DataMasks", menuName = "Scriptable Objects/DataMasks")]
public class DataMasks : ScriptableObject
{
    public string Name;
    public int maskID;
}

