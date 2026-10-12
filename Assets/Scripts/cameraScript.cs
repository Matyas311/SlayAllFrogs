using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(-15f, 17f, -15f);

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(27.5f, 45f, 0f);
    }
}
