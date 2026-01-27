using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string Name;
    public float Distance;

    [System.Serializable]
    public class Audio
    {
        public AudioSource Sound;
    }

public AudioClip[] Sounds;    
}