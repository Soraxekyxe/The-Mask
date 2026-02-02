using UnityEngine;
using UnityEngine.EventSystems;

public class ElectricityClickable : MonoBehaviour, IPointerDownHandler
{
    public ElectricityManager electricityManager;

    public void OnPointerDown(PointerEventData click)
    {
        electricityManager.OpenCanvas();
    }
}