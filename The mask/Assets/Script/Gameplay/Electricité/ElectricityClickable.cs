using UnityEngine;

public class ElectricityClickable : MonoBehaviour
{
    public ElectricityManager electricityManager;

    private void OnMouseDown()
    {
        electricityManager.OpenCanvas();
    }
}