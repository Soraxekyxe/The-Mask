using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yuu
{
    public class ButtonStart : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField] private int sceneIndex = 0;

        [Header("Image Panel")]
        [SerializeField] private GameObject imagePanel;

        void Awake()
        {
            if (imagePanel != null)
                imagePanel.SetActive(false);
        }
        
        public void OnStartClick()
        {
            Debug.Log("Load scene index: " + sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }
        
        public void OnExitClick()
        {
#if UNITY_EDITOR
            Debug.Log("Exit Play Mode");
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        public void ShowImage()
        {
            if (imagePanel != null)
                imagePanel.SetActive(true);
        }
        
        public void HideImage()
        {
            if (imagePanel != null)
                imagePanel.SetActive(false);
        }
    }
}
