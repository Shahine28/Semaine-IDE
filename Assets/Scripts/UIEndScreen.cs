using UnityEngine;
using EndGameManagerSpace;
using UnityEngine.UI;

public class UIEndScreen : MonoBehaviour
{
    [Header("UIElements")]
    [SerializeField] GameObject m_winScreen;
    [SerializeField] GameObject m_loseScreen;
    [SerializeField] GameObject m_playButton;
    [SerializeField] GameObject m_quitButton;

    [Header("Play Sprite")]
    [SerializeField] Sprite m_playBlue;

    bool m_isPlayPressed = false;

    private void Start()
    {
        m_winScreen.SetActive(false);
        m_loseScreen.SetActive(false);
        m_playButton.SetActive(false);
        m_quitButton.SetActive(false);
    }

    public void ActiveElements(bool isDead)
    {
        if (isDead) m_loseScreen.SetActive(true);
        else if (!isDead) m_winScreen.SetActive(true);

        m_playButton.SetActive(true);
        m_quitButton.SetActive(true);
    }

    public void PressPlay()
    {
        if (!m_isPlayPressed)
        {
            EndGameManager.Instance.ReplayCondition();
            m_playButton.GetComponent<Image>().sprite = m_playBlue;

            m_isPlayPressed = true;
        }
    }

    public void PressQuit()
    {
        EndGameManager.Instance.QuitGame();
    }
}
