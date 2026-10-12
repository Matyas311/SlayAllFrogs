
using UnityEngine;

public class RotateBarrel : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    void Update()
    {
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 lookTarget = ray.GetPoint(distance);
            lookTarget.y = transform.position.y;

            Vector3 direction = lookTarget - transform.position;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        }
    }
}
