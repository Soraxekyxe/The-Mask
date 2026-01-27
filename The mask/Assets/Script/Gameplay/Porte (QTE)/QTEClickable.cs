using UnityEngine;

public class QTEClickable : MonoBehaviour
{
    public QTEManager qteManager;

    private void OnMouseDown()
    {
        if (qteManager != null)
            qteManager.StartQTE();
    }
}