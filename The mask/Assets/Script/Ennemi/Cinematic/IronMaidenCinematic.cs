using System.Collections;
using UnityEngine;

public class IronMaidenCinematic : MonoBehaviour
{
    public GameObject ironMaidenSprite;
    
    public Vector3 startPos;   // position de départ
    public Vector3 endPos;     // position d’arrivée
    public float duration = 3f; // durée de la cinématique en secondes

   
    public void play()
    {
        ironMaidenSprite.SetActive(true);
        StartCoroutine(CinematicMove());
        
        Debug.Log("Lance la cinématique");
    }

    IEnumerator CinematicMove()
    {
        Debug.Log("Cinematic est lancer");
        float elapsed = 0f;
        ironMaidenSprite.transform.position = startPos;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            ironMaidenSprite.transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null; // attendre la frame suivante
        }
        
        ironMaidenSprite.SetActive(false);
        ironMaidenSprite.transform.position = endPos;  // sécurité : fin parfaite
    }
}
