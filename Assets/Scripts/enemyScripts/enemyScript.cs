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
    public static event Action OnEnemyDeath;
    private float enemyRotateSpeed = 100.0f;
    private bool isDead;


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

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        HP -= damage;
        if (HP <= 0f)
        {
            isDead = true;
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;

        bulletScript bullet = other.GetComponent<bulletScript>();
        TakeDamage(bullet.bulletDamage);
        ElementEffects.OnHit(bullet.element, this, transform.position, bullet.bulletDamage);
        Destroy(other.gameObject);

    }
}
