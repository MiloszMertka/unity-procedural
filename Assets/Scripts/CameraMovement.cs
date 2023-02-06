using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0.01f, 1)]
    public float speedFactor = 0.1f;

    [Range(0.01f, 3)]
    public float zoomSpeedFactor = 0.1f;

    public float minCameraSize = 10;
    public float maxCameraSize = 20;

    private Camera mainCamera;
    private Vector3 previousMousePosition = Vector3.zero;
    private bool primaryMouseButtonDown = false;

    private static readonly int PRIMARY_MOUSE_BUTTON_CODE = 0;

    public void Start()
    {
        mainCamera = Camera.main;
    }

    public void LateUpdate()
    {
        MoveCamera();
        SetCameraZoom();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButton(PRIMARY_MOUSE_BUTTON_CODE))
        {
            Vector3 mousePosition = Input.mousePosition;

            if (!primaryMouseButtonDown)
            {
                previousMousePosition = mousePosition;
                primaryMouseButtonDown = true;
            }

            Vector3 inversedMouseShift = previousMousePosition - mousePosition;
            mainCamera.transform.Translate(inversedMouseShift * speedFactor);
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(PRIMARY_MOUSE_BUTTON_CODE))
        {
            primaryMouseButtonDown = false;
        }
    }

    private void SetCameraZoom()
    {
        float mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");
        float zoomValue = mouseScrollValue * zoomSpeedFactor;
        float zoomedCameraSize = mainCamera.orthographicSize + zoomValue;

        mainCamera.orthographicSize = Mathf.Clamp(zoomedCameraSize, minCameraSize, maxCameraSize);
    }
}
