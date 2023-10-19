using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public delegate void OnDeathDelegate();
    public static event OnDeathDelegate OnDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Laser"))
        {
            //StopPlayer
            StartCoroutine(DeathAnimation());
            //OnDeath.Invoke();
            Destroy(gameObject);
        }
    }

    IEnumerator DeathAnimation()
    {

        yield return null;
    }
}
