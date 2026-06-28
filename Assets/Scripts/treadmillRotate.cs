using UnityEngine;

public class TreadmillRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float modelOffset = 90f;

    private PlayerMovement playerMovement;
    private float initialX;
    private float initialZ;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        initialX = transform.localEulerAngles.x;
        initialZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        Vector3 moveDirection = playerMovement.MoveDirection;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + modelOffset;
            Quaternion targetRotation = Quaternion.Euler(initialX, angle, initialZ);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}