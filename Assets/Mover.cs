using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private bool m_enabled = true;

    [SerializeField] private ArticulationController m_frontLeftLegStepper;
    [SerializeField] private ArticulationController m_frontRightLegStepper;
    [SerializeField] private ArticulationController m_backLeftLegStepper;
    [SerializeField] private ArticulationController m_backRightLegStepper;

    [SerializeField] private float m_speed = 2f;

    [SerializeField] private Transform m_arrow;
    [SerializeField] private float m_arrowDistance = 2f;

    public Vector3 LastDirection => m_lastDirection;

    private Vector3 m_lastDirection;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

   /* void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Vector3 startPosition = this.transform.position;

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-1f * Vector3.forward * m_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * m_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * m_speed * Time.deltaTime);
            }

            Vector3 endPosition = this.transform.position;

            m_lastDirection = endPosition - startPosition;

            Debug.DrawRay(this.transform.position, m_lastDirection, Color.blue, 0.5f);

            m_arrow.position = this.transform.position + m_lastDirection.normalized * m_arrowDistance;
        }
    }*/

    private IEnumerator LegUpdateCoroutine()
    {
        while (true)
        {
            if(m_enabled)
            {
                do
                {
                    m_frontLeftLegStepper.TryMove();
                    m_backRightLegStepper.TryMove();
                
                    yield return null;

                } while (m_backRightLegStepper.IsMoving || m_frontLeftLegStepper.IsMoving);

                do
                {
                    m_frontRightLegStepper.TryMove();
                    m_backLeftLegStepper.TryMove();

                    yield return null;
                } while (m_backLeftLegStepper.IsMoving || m_frontRightLegStepper.IsMoving);
            }
            else
            {
                yield return null;
            }
        }
    }
}
