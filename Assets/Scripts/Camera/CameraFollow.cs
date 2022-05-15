using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public new Camera camera;
    public Transform Target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        camTransform.position = Target.position + Offset;
    }
    
    private void LateUpdate()
    {
        if (camera.enabled)
        {
            MoveTo();
        }
    }

    public void MoveTo()
    {
        Vector3 targetPosition = Target.position + Offset;
        var position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        camTransform.position = position;
    }
}