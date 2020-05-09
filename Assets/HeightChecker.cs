using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    public float Height => m_height;

    [SerializeField] float m_height = 0.15f;

    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 20f))
        {
            if(hit.transform != this.transform)
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + m_height, this.transform.position.z);
            }
        }
    }
}
