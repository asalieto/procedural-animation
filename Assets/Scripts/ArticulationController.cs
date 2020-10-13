using System.Collections;
using UnityEngine;

public class ArticulationController : MonoBehaviour
{
    public bool IsMoving => m_moving;

    public void TryMove()
    {
        if (m_moving)
        {
            return;
        }

        if (Vector3.Distance(m_target.position, m_feet.position) >= m_distanceToStep)
        {
            m_moving = true;

            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        Quaternion startRot = m_feet.rotation;
        Vector3 startPoint = m_feet.position;

        Quaternion endRot = m_target.rotation;
        Vector3 endPoint = m_target.position;

        //TODO improve, just testing
        var heightChecker = m_target.GetComponent<HeightChecker>();

        if (heightChecker != null)
        {
            endPoint = new Vector3(endPoint.x, endPoint.y - heightChecker.Height, endPoint.z);
        }

        Vector3 centerPoint = (startPoint + endPoint) / 2;
        centerPoint += m_target.up * Vector3.Distance(startPoint, endPoint) / 2f;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / m_moveDuration;
             normalizedTime = Easing.Cubic.InOut(normalizedTime);

            //m_feet.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
            //m_feet.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            m_feet.position =
              Vector3.Lerp(
                Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                normalizedTime
              );

            yield return null;
        }
        while (timeElapsed < m_moveDuration);

        m_moving = false;
    }

    private bool m_moving = false;

    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_feet;

    [SerializeField] private float m_distanceToStep = 1f;
    [SerializeField] private float m_moveDuration = 0.3f;
}
