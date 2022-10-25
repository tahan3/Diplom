using System;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 axis;

    private void Awake()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(cameraTransform);
    }
}