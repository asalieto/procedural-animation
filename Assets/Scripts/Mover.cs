using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 LastDirection => m_lastDirection;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

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

    private Vector3 m_lastDirection;

    [SerializeField] private bool m_enabled = true;

    [SerializeField] private ArticulationController m_frontLeftLegStepper;
    [SerializeField] private ArticulationController m_frontRightLegStepper;
    [SerializeField] private ArticulationController m_backLeftLegStepper;
    [SerializeField] private ArticulationController m_backRightLegStepper;
}
