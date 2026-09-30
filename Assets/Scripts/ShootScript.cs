using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrelTip;
    public float barrelHeight = 0.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = barrelTip.forward;
            direction.y = 0f;
            direction.Normalize();

            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, rotation);
            bullet.GetComponent<bulletScript>().SetDirection(direction);
        }
    }
}