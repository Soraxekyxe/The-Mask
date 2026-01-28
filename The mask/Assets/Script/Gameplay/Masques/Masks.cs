using System;
using UnityEngine;

[Serializable]
public class MaskData
{
    //pour dans l'inspecteur
    public string id;
    public string displayName;

    [TextArea]      //okazou on veut dire c koi
    public string description;
    
    
    //la pour rajj nos trucs d'identification
    public Sprite icon;
    public GameObject prefab;
    public AudioClip equipSound;
}