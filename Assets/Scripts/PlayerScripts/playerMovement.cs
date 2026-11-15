using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }
    private CharacterController controller;
    private float verticalVelocity;
    [SerializeField] PlayerStats stats;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = Vector3.ClampMagnitude(move, 1f);
        MoveDirection = move;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += -9.81f * Time.deltaTime;

        Vector3 velocity = move * stats.moveSpeed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}