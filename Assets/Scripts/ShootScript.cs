using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera mainCamera;
    public LayerMask groundLayer;
    public float barrelHeight = 2.7f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                Vector3 direction = (hit.point - transform.position);
                direction.y = 0f;
                direction.Normalize();

                Quaternion rotation = Quaternion.LookRotation(direction);
                Vector3 spawnPosition = transform.position + direction * 2f + Vector3.up * barrelHeight;
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
                bullet.GetComponent<bulletScript>().SetDirection(direction);
            }
        }
    }
}