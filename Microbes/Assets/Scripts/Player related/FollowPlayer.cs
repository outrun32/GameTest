using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 1f;
    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {
        if (target == null)
        {
            target = this.transform;
        }
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothPosition;
    }
}
