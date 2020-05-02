using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    [SerializeField] float height = 0.15f;

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 20f))
        {
            if(hit.transform != this.transform)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.Log("Did Hit with " + hit.collider.name);

                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + height, this.transform.position.z);
            }
        }
    }
}
