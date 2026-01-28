using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yuu
{
    public class ButtonStart : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = 0;

        public void OnStartClick()
        {
            Debug.Log("nrmlmt c bon tu load ce que tu veut");
            SceneManager.LoadScene(sceneIndex);
        }

        public void OnExitClick()
        {
#if UNITY_EDITOR
            Debug.Log("okazou pr vr si c oké");
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}