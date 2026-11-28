using System;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ElementBullet[] elementBullets;
    [SerializeField] Transform barrelTip;
    [SerializeField] PlayerStats stats;
    [SerializeField] playerLoadout loadout;

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
            Element current = loadout.ActiveElement;
            GameObject prefab = GetPrefab(current);
            GameObject bullet = Instantiate(prefab, barrelTip.position, rotation);
            bullet.GetComponent<bulletScript>().Init(direction, stats.damage, stats.projectileSpeed, current);
        }
    }

    GameObject GetPrefab(Element element)
    {
        foreach (ElementBullet eb in elementBullets)
            if (eb.element == element && eb.prefab != null)
                return eb.prefab;

        return bulletPrefab;
    }
}