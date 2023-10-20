using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_boostList;
    [SerializeField] float m_timeInterval = 20f;
    float m_currentTime = 0f;

    [SerializeField] float minX, maxX, minZ, maxZ;
    float spawnOffset = 10f;

    private void FixedUpdate()
    {
        m_currentTime += Time.fixedDeltaTime;

        if (m_currentTime >= m_timeInterval)
        {
            InitializeBoost();
            m_currentTime -= m_timeInterval;
        }
    }

    private void InitializeBoost()
    {
        GameObject current = Instantiate(m_boostList[Random.Range(0, m_boostList.Length)]);
        current.transform.position = new Vector3(Random.Range(minX + spawnOffset, maxX - spawnOffset),
                                                 5f, 
                                                 Random.Range(minZ + spawnOffset, maxZ - spawnOffset));
    }
}
