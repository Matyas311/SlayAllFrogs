using System;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTip;
    [SerializeField] PlayerStats stats;

    private float lastShotTime = 0.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= stats.fireRate)
        {
            lastShotTime = Time.time;

            Vector3 direction = transform.forward;
            direction.y = 0f;
            direction.Normalize();

            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, rotation);
            bullet.GetComponent<bulletScript>().Init(direction, stats.damage, stats.projectileSpeed);
        }
    }
}