using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] public UIEndScreen m_CanvasParent;
    public GameObject enemie;
    bool m_isDead = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Laser") && !m_isDead)
        {
            m_isDead = true;
            m_CanvasParent.gameObject.SetActive(true);
            enemie.GetComponent<Death>().m_CanvasParent.gameObject.SetActive(true);
            EndScreen(true);
            enemie.GetComponent<Death>().EndScreen(false);
            StartCoroutine(DeathAnimation());
        }
    }

    public void EndScreen(bool state)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_CanvasParent.ActiveElements(state);
    }


    IEnumerator DeathAnimation()
    {
        Time.timeScale = Mathf.Lerp(1.0f, 0.0f, 1f);
        Destroy(gameObject);
        yield return null;
    }
}
