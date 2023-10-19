using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    [SerializeField] int m_boostIndex = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float m_timeInterval = 20f;
    float m_currentTime = 0f;

    string[] m_boostTypes =
    {
        "Dash",
        "Invisibility"
    };

    private void Start()
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

    private void FixedUpdate()
    {
        m_currentTime += Time.fixedDeltaTime;

        if (m_currentTime >= m_timeInterval)
        {
            Destroy(gameObject);
        }
    }

    private void SpawningBehavior()
    {
        Debug.Log("Spawned");
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer);

        transform.position = hit.point + new Vector3(0f, 1.5f, 0f);
    }
}
