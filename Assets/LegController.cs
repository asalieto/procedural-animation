using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    // The position and rotation we want to stay in range of
    [SerializeField] Transform body;

    // The position and rotation we want to stay in range of
    [SerializeField] Transform homeTransform;

    [SerializeField] float towardsDistance;

    // Stay within this distance of home
    [SerializeField] float wantStepAtDistance;
    // How long a step takes to complete
    [SerializeField] float moveDuration;

    // Is the leg moving?
    public bool Moving;

    // Coroutines must return an IEnumerator
    IEnumerator MoveToHome()
    {
        // Indicate we're moving (used later)
        Moving = true;

        // Store the initial conditions
        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = homeTransform.rotation;
        Vector3 endPoint = homeTransform.position;

        Debug.Log("endPoint: " + endPoint);
        Debug.Log("homeTransform.forward: " + homeTransform.forward);
        Debug.Log("homeTransform.forward * towardsDistance: " + homeTransform.forward * towardsDistance);
        Debug.Log("endPoint + homeTransform.forward * towardsDistance: " + (endPoint + homeTransform.forward * towardsDistance));
       // endPoint = endPoint + homeTransform.forward * towardsDistance;
        endPoint = endPoint + body.forward * towardsDistance;

        // Time since step started
        float timeElapsed = 0;

        // Here we use a do-while loop so the normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before ending.
        do
        {
            // Add time since last frame to the time elapsed
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / moveDuration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveDuration);

        // Done moving
        Moving = false;
    }


    // New variable!
    // Fraction of the max distance from home we want to overshoot by
    [SerializeField] float stepOvershootFraction;

    // Was previously void Update()
    public void TryMove()
    {
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, homeTransform.position);

        // If we are too far off in position or rotation
        if (distFromHome > wantStepAtDistance)
        {
            StartCoroutine(MoveToHome());
        }
    }

    /*
    void Update()
    {
        // If we are already moving, don't start another move
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, homeTransform.position);

        // If we are too far off in position or rotation
        if (distFromHome > wantStepAtDistance)
        {
            // Start the step coroutine
            //StartCoroutine(MoveToHome());
            StartCoroutine(MoveToHome());
        }
    }
    */
}
