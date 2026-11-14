using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float bulletDamage;
    public float maxDistance = 30.0f;

    private float speed;
    private Vector3 direction;
    private Vector3 spawnPosition;

    public void Init(Vector3 dir, float damage, float projectileSpeed)
    {
        direction = dir;
        bulletDamage = damage;
        speed = projectileSpeed;
        spawnPosition = transform.position;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, spawnPosition) >= maxDistance)
            Destroy(gameObject);
    }
}