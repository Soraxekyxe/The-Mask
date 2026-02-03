using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string Name;
    public int monsterID;
    public float Distance;
    public float Walk;
    public AudioClip Scream;

    [System.Serializable]
    public class Audio
    {
        public AudioClip Sound;
    }

public AudioClip[] Sounds;    
}