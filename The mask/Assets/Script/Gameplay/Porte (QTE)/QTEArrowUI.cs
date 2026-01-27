using UnityEngine;
using UnityEngine.UI;

public class QTEArrowUI : MonoBehaviour
{
    public Image img;

    [Header("Colors")]
    public Color neutral = Color.white;
    public Color correct = Color.green;
    public Color wrong = Color.red;

    void Reset()
    {
        img = GetComponent<Image>();
    }

    public void SetSprite(Sprite s)
    {
        if (img == null) img = GetComponent<Image>();
        img.sprite = s;
    }

    public void SetNeutral()
    {
        if (img == null) img = GetComponent<Image>();
        img.color = neutral;
    }

    public void SetCorrect()
    {
        if (img == null) img = GetComponent<Image>();
        img.color = correct;
    }

    public void SetWrong()
    {
        if (img == null) img = GetComponent<Image>();
        img.color = wrong;
    }
}