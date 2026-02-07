using UnityEngine;

public class TestCinématic : MonoBehaviour
{
    public IronMaidenCinematic cinematic;
    public ClownCinematic clownCinematic;
    public AngelCinematic angelCinematic;

    void Start()
    {
        cinematic.play();
        clownCinematic.play();
        angelCinematic.play();
    }
}
