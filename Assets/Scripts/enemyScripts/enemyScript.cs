using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] float HP;

    public float contactDamage = 12f;

    private float maxHP;
    private GameObject player;
    public float enemyMoveSpeed;
    public static event Action OnEnemyDeath;
    private float enemyRotateSpeed = 100.0f;
    private bool isDead;

    //Fire related
    private int burnTicksLeft;
    private float burnTimer;
    private float burnPercent;

    //Ground related
    private float slowTimeLeft;
    private float slowMultiplier = 1f;

    //Wind related
    private Vector3 knockbackVelocity;
    private float knockbackDrag = 24f;




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHP = HP;

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
                transform.position += direction * enemyMoveSpeed * slowMultiplier * Time.deltaTime;
        }
        HandleBurn();
        HandleSlow();
        HandleKnockback();
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
        ElementEffects.OnHit(bullet.element, this, transform.position, bullet.bulletDamage, bullet.Direction);
        Destroy(other.gameObject);

    }

    //////////////////////////////////////////////////////////////////////////////////////
    ////////FIRE RELATED FUNCTIONS////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////

    public void ApplyBurn(int ticks, float percentOfHP)
    {
        if (burnTicksLeft <= 0) burnTimer = 0f;
        burnTicksLeft = ticks;
        burnPercent = percentOfHP;

    }

    void HandleBurn()
    {
        if (burnTicksLeft <= 0) return;

        burnTimer += Time.deltaTime;
        if (burnTimer >= 1f)
        {
            burnTimer -= 1f;
            burnTicksLeft--;
            TakeDamage(maxHP * burnPercent);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////
    ////////GROUND ELEMENT RELATED FUNCTIONS//////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////
    public void ApplySlow(float seconds, float speedMultiplier)
    {
        slowMultiplier = speedMultiplier;
        slowTimeLeft = seconds;
    }

    void HandleSlow()
    {
        if (slowTimeLeft <= 0f) return;

        slowTimeLeft -= Time.deltaTime;
        if (slowTimeLeft <= 0f)
        {
            slowMultiplier = 1f;
        }
    }



    //////////////////////////////////////////////////////////////////////////////////////
    ////////WIND ELEMENT RELATED FUNCTIONS////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////
    public void ApplyKnockback(Vector3 direction, float force)
    {
        direction.y = 0f;
        knockbackVelocity = direction.normalized * force;
    }

    void HandleKnockback()
    {
        if (knockbackVelocity == Vector3.zero) return;

        transform.position += knockbackVelocity * Time.deltaTime;
        knockbackVelocity = Vector3.MoveTowards(knockbackVelocity, Vector3.zero, knockbackDrag * Time.deltaTime);
    }
}
