using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_boostList;
    [SerializeField] float m_timeInterval = 10f;
    float m_currentTime = 0f;

    [SerializeField] float minX, maxX, minY, maxY;

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
        GameObject current = Instantiate(m_boostList[Random.Range(1, m_boostList.Length) - 1]);
        current.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 20f);
    }
}
