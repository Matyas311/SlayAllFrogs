using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    private float spawnRadius = 40.0f;
    private Vector3 spawnPos;
    private float spawnRate = 5.0f;
    private float lastSpawnTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnRate)
        {
            lastSpawnTime = Time.time;
            float randomAngle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
            spawnPos = new Vector3(transform.position.x + spawnRadius * Mathf.Sin(randomAngle), transform.position.y, transform.position.z + spawnRadius * Mathf.Cos(randomAngle));

            Instantiate(enemyPrefab, spawnPos, transform.rotation);
        }
    }
}
