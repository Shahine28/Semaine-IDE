using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] UIEndScreen m_CanvasParent;
    bool m_isDead = false;

    public delegate void OnDeathDelegate();
    public static event OnDeathDelegate OnDeath;

    private void OnEnable()
    {
        Death.OnDeath += EndScreen;
    }
    private void OnDisable()
    {
        Death.OnDeath -= EndScreen;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Laser") && !m_isDead)
        {
            m_isDead = true;
            OnDeath.Invoke();

            StartCoroutine(DeathAnimation());
        }
    }

    private void EndScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_CanvasParent.ActiveElements(m_isDead);
    }


    IEnumerator DeathAnimation()
    {


        Time.timeScale = Mathf.Lerp(1.0f, 0.0f, 4);
        Destroy(gameObject);
        yield return null;
    }
}
