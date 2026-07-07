using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed = 20.0f;
    public float bulletDamage = 5.0f;
    public float maxDistance = 30.0f;

    private Vector3 direction;
    private Vector3 spawnPosition;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        spawnPosition = transform.position;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, spawnPosition) >= maxDistance)
            Destroy(gameObject);
    }
}
