using System.Collections;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTip;

    private float fireRate = 1.0f;
    private float lastShotTime = 0.0f;



    void Start()
    {
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= fireRate)
        {
            lastShotTime = Time.time;
            Vector3 direction = barrelTip.forward;
            direction.y = 0f;
            direction.Normalize();

            Debug.Log(direction);

            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, rotation);
            bullet.GetComponent<bulletScript>().SetDirection(direction);
        }
    }
}