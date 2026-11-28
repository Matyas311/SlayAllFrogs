using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float bulletDamage;
    public float maxDistance = 30.0f;
    public Element element;

    private float speed;
    private Vector3 direction;
    public Vector3 Direction => direction;
    private Vector3 spawnPosition;

    public void Init(Vector3 dir, float damage, float projectileSpeed, Element elem)
    {
        direction = dir;
        bulletDamage = damage;
        speed = projectileSpeed;
        spawnPosition = transform.position;
        element = elem;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, spawnPosition) >= maxDistance)
            Destroy(gameObject);
    }
}