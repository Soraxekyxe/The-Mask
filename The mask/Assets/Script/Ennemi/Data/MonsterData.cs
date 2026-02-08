using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public int monsterID;
    
    [Header("Variables")]
    public float Distance;
    public float maxWalk;
    public float minWalk;
    
    
    [Header("Sounds")]
    public AudioClip laugh;
    public AudioClip Scream;

    [System.Serializable]
    public class Audio
    {
        public AudioClip Sound;
    }

public AudioClip[] Sounds;    
}