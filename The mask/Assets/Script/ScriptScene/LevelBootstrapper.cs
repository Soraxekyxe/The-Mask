using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBootstrapper : MonoBehaviour
{
    void Start()
    {
        LevelSession.Begin(SceneManager.GetActiveScene().name);
    }
}