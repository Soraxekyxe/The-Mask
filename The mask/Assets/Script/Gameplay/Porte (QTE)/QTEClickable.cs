using UnityEngine;
using UnityEngine.EventSystems;
public class QTEClickable : MonoBehaviour, IPointerDownHandler
{
    public QTEManager qteManager;

    public void OnPointerDown(PointerEventData click)
    {
        if (qteManager != null)
            qteManager.StartQTE();
    }
}