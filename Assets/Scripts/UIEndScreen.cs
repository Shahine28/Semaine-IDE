using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndScreen : MonoBehaviour
{
    [Header("UIElements")]
    [SerializeField] GameObject m_winScreen;
    [SerializeField] GameObject m_loseScreen;
    [SerializeField] GameObject m_playButton;
    [SerializeField] GameObject m_quitButton;

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
}
