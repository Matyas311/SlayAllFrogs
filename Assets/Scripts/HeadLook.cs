using UnityEngine;

public class HeadLook : MonoBehaviour
{
    [Header("Settings")]
    public Camera mainCamera;
    public LayerMask groundLayer;

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 lookTarget = hit.point;
            lookTarget.y = transform.position.y; // keep head level, no up/down tilt

            Vector3 direction = lookTarget - transform.position;

            if (direction.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}