using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset")]
    public Vector3 offset = new Vector3(-15f, 38f, -15f);

    [Header("Zoom")]
    public bool enableZoom = true;
    public float zoomSpeed = 2f;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;

    private float currentZoom = 1f;
    private Quaternion fixedRotation;

    void Start()
    {
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleZoom();

        transform.position = target.position + offset * currentZoom;
        transform.rotation = fixedRotation;
    }

    void HandleZoom()
    {
        if (!enableZoom) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }
}