using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera mainCamera;
    public LayerMask groundLayer;
    public Transform barrelTip;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                Vector3 direction = (hit.point - barrelTip.position);
                direction.y = 0f;
                direction.Normalize();

                Quaternion rotation = Quaternion.LookRotation(direction);
                GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, rotation);
                bullet.GetComponent<bulletScript>().SetDirection(direction);
            }
        }
    }
}