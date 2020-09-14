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

    void Update()
    {
        CameraMovement();
        ClampCamera();
    }

    private void CameraMovement()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(0))
        {
            LogStartPosition();
        }

        if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }
    }

    private void LogStartPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            startPosition = hitInfo.point;
        }
    }

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

    private void ClampCamera()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;
    }
}
