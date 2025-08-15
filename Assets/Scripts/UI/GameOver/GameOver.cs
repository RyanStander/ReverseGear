using UnityEngine;

namespace UI.GameOver
{
    public class GameOver : MonoBehaviour
    {
        public void LoadGameOverScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }
}
