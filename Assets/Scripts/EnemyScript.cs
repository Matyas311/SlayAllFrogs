using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] float HP;
    public float contactDamage = 12f;

    private GameObject player;
    public float enemyMoveSpeed = 1.0f;
    private float enemyRotateSpeed = 100.0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (player)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            float distance = direction.magnitude;
            direction.Normalize();

            float singleStep = enemyRotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (distance > 7f)
                transform.position += direction * enemyMoveSpeed * Time.deltaTime;
        }
    }

    void TakeDamage(float Damage)
    {
        float newHP = HP - Damage;
        if (newHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            HP = newHP;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(other.GetComponent<bulletScript>().bulletDamage);
            Destroy(other.gameObject);
        }
    }
}
