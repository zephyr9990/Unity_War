using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float scrollSpeed = .5f;

    private Vector3 startPosition;
    private bool startPosWasOnUI = false;

    void Update()
    {
        CameraMovement();
        ClampCamera();
    }

    /// <summary>
    /// Controls camera movement.
    /// </summary>
    private void CameraMovement()
    {
        // If player has clicked on UI, do not scroll.
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            LogStartPosition();
            startPosWasOnUI = false;
        }
        else if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            startPosWasOnUI = true;
            return;
        }

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()
            && !startPosWasOnUI)
        {
            MoveCamera();
        }
    }

    /// <summary>
    /// Logs the start position of where player has clicked.
    /// </summary>
    private void LogStartPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            startPosition = hitInfo.point;
        }
    }

    /// <summary>
    /// Tracks the player's cursor position and moves the camera accordingly.
    /// </summary>
    private void MoveCamera()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPosition = transform.position;
            targetPosition.z += -(hitInfo.point.z - startPosition.z) * scrollSpeed;
            transform.position = targetPosition;
        }
    }

    /// <summary>
    /// Clamps the camera.
    /// </summary>
    private void ClampCamera()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;
    }
}
