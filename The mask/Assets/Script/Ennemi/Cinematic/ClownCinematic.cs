using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ClownCinematic : MonoBehaviour
{
   
    public GameObject clownSprite;
    public Vector3 startEuler;
    public Vector3 endEuler;
    public float duration = 3f;
    

    public void play()
    {
        clownSprite.SetActive(true);
        StartCoroutine(Cinematic());
    }

    IEnumerator Cinematic()
    {
        Quaternion qStart = Quaternion.Euler(startEuler);
        Quaternion qEnd   = Quaternion.Euler(endEuler);

        // ALLER
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            clownSprite.transform.rotation = Quaternion.Slerp(qStart, qEnd, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(2f);

        // RETOUR
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            clownSprite.transform.rotation = Quaternion.Slerp(qEnd, qStart, t);
            yield return null;
        }

        // Fin
        clownSprite.transform.rotation = qStart;
        clownSprite.SetActive(false);
        
    }
}

