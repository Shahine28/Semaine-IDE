using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    [SerializeField] int m_boostIndex = 0;

    string[] m_boostTypes =
{
        //"Jump",
        "Speed",
        "Invisibility"
    };

    private void OnEnable()
    {
        SpawningBehavior();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerPower>(out PlayerPower component))
        {
            bool willDestroy = component.RecoverCollectable(m_boostTypes[m_boostIndex]);

            if (willDestroy) { Destroy(gameObject); }
        }
    }

    private void SpawningBehavior()
    {

    }
}
