using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float distanceForPayload = .5f;
    [SerializeField] private GameObject payload;
    private bool payloadDropped = false;
    private void Update()
    {
        // Move forward towards the target destination
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        if (NearTargetDestination())
        {
            DropPayload();
        }
    }

    /// <summary>
    /// Drops the payload below plane.
    /// </summary>
    private void DropPayload()
    {
        Instantiate(payload, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        payloadDropped = true;
    }

    /// <summary>
    /// Checks to see if the plane is near enough to destination.
    /// </summary>
    /// <returns>Returns true if player is close enough to drop payload, false if not.</returns>
    private bool NearTargetDestination()
    {
        Vector3 toTarget = transform.parent.position - transform.position;
        return toTarget.magnitude < distanceForPayload && !payloadDropped;
    }
}
