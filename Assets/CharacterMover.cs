﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.CharacterController))]
public class CharacterMover : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;

    void Update()
    {
        UnityEngine.CharacterController controller = GetComponent<UnityEngine.CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
    }
}