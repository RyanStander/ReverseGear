using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartGame : MonoBehaviour
    {
        public void LoadLevel()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
