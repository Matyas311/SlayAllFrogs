using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset")]
    public Vector3 offset = new Vector3(-15f, 38f, -15f);


    private float currentZoom = 2f;
    private Quaternion fixedRotation;

    void Start()
    {
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset * currentZoom;
        transform.rotation = fixedRotation;
    }

}