using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    [Range(1, 100)] public float smoothSpeed = 10f;
    public Vector3 Offset;
    // if put this mechanism on Update,
    // it will be zittery. because movement is playing on Update

    private void LateUpdate()
    {
        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(Target);
    }

}
