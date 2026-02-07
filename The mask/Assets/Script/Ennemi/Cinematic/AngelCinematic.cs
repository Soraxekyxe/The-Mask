using System.Collections;
using UnityEngine;

public class AngelCinematic : MonoBehaviour
{
    public GameObject angelSprite;

    public int blinkCount = 4;        // nombre de clignotements
    public float blinkSpeed = 0.2f;   // vitesse d’un clignotement
    public float fadeDuration = 1f;   // durée du fondu

    private SpriteRenderer sr;
    

    private void Awake()
    {
        sr = angelSprite.GetComponent<SpriteRenderer>();
    }

    public void play()
    {
        angelSprite.SetActive(true);

        // rendre visible au début
        Color c = sr.color;
        c.a = 1f;
        sr.color = c;

        StartCoroutine(Cinematic());
    }

    IEnumerator Cinematic()
    {
        // --- Clignotement ---
        yield return StartCoroutine(BlinkEffect());

        // --- Fade-out ---
        yield return StartCoroutine(FadeOut());

        angelSprite.SetActive(false);
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // invisible
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            yield return new WaitForSeconds(blinkSpeed);

            // visible
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        float elapsed = 0f;
        Color c = sr.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = 1f - (elapsed / fadeDuration);
            sr.color = c;
            yield return null;
        }
        
    }
}
