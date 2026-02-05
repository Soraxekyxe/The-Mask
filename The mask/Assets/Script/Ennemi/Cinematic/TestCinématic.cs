using UnityEngine;

public class TestCinématic : MonoBehaviour
{
    public IronMaidenCinematic cinematic;
    public ClownCinematic clownCinematic;

    void Start()
    {
        cinematic.play();
        clownCinematic.play();
    }
}
