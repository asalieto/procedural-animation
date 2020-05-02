using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegResizer : MonoBehaviour
{
    [SerializeField]
    private Transform m_startPoint;
    [SerializeField]
    private Transform m_endPoint;

    [ContextMenu("Resize")]
    public void Resize()
    {
        Vector3 centerPos = m_startPoint.position + (m_endPoint.position - m_startPoint.position) / 2;

        this.transform.position = centerPos;
        this.transform.LookAt(m_endPoint);

        
        float scale = Mathf.Abs(Vector3.Distance(m_startPoint.position, m_endPoint.position));

        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, scale);
    }
}
