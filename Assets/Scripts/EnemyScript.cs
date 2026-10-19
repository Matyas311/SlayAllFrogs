using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private GameObject player;
    public float enemyMoveSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > 2.3f)
                transform.position += direction * enemyMoveSpeed * Time.deltaTime;

        }

    }
}
