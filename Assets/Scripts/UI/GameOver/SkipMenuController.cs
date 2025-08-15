using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver
{
    public class SkipMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Button skipButton;

        private void Start()
        {
            menuPanel.SetActive(false);
            skipButton.onClick.AddListener(OpenMenu);
        }

        private void OpenMenu()
        {
            menuPanel.SetActive(true);
            skipButton.gameObject.SetActive(false);
        }

        public void RetryGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }

        public void MainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
