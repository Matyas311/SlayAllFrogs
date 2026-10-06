
using UnityEngine;

public class RotateBarrel : MonoBehaviour
{
    private float sensitivity = 2f;
    public float currentYRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        currentYRotation += mouseX * sensitivity;

        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
}
