using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class CarréClickable : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;
    [Header("Visibility")]
    public float hiddenAlpha = 0f;
    public float hoverAlpha = 0.6f;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        SetAlpha(hiddenAlpha);
    }
    void Update()
    {
        if (UIState.IsAnyPopupOpen)
        {
            col.enabled = false;
            SetAlpha(0f);
            return;
        }
        col.enabled = true;
    }
    void OnMouseEnter()
    {
        if (UIState.IsAnyPopupOpen) return;
        SetAlpha(hoverAlpha);
    }
    void OnMouseExit()
    {
        SetAlpha(hiddenAlpha);
    }
    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}