using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGameManagerSpace
{
    public class EndGameManager : MonoBehaviour
    {
        public static EndGameManager Instance;

        private int m_replayCount = 0;

        private void Awake()
        {
            Instance = this;
            m_replayCount = 0;
        }

        public void ReplayCondition()
        {
            m_replayCount++;

            if (m_replayCount >= 2)
            {
                SceneManager.LoadScene(1);
                Time.timeScale = 1.0f;
            }
        }

        public void QuitGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}

