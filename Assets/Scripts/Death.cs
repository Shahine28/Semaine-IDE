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
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Laser"))
        {
            m_isDead = true;
            //StopPlayer
            OnDeath.Invoke();

            StartCoroutine(DeathAnimation());
            Destroy(gameObject);
        }
    }

    private void EndScreen()
    {
        m_CanvasParent.ActiveElements(m_isDead);
    }


    IEnumerator DeathAnimation()
    {

        yield return null;
    }
}
