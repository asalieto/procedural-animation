using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private ArticulationController m_frontLeftLegStepper;
    [SerializeField] private ArticulationController m_frontRightLegStepper;
    [SerializeField] private ArticulationController m_backLeftLegStepper;
    [SerializeField] private ArticulationController m_backRightLegStepper;

    [SerializeField] private float m_speed = 2f;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

    void Update()
    {
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
    }

    private IEnumerator LegUpdateCoroutine()
    {
        while (true)
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
    }
}
