using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    void OnEnable()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_timer = m_wanderTimer;
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, m_wanderRadius, -1);
            m_agent.SetDestination(newPos);
            m_timer = 0;
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private NavMeshAgent m_agent;
    private float m_timer;

    [SerializeField] private float m_wanderRadius = 7f;
    [SerializeField] private float m_wanderTimer = 5f;
}