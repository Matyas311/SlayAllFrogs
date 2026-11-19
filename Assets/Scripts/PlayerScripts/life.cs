using UnityEngine;
using System;
public class Life : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] float iFrames = 1f;

    private float nextDamageTime = 0f;
    private float currentHP;
    public event Action<float, float> OnHealthChanged; // current, max
    public float CurrentHP => currentHP;
    private bool isDead = false;

    public event Action OnGameOver;

    void Start()
    {
        currentHP = stats.maxHP;
        OnHealthChanged?.Invoke(currentHP, stats.maxHP);
    }

    void OnTriggerStay(Collider other)
    {
        if (isDead || Time.time < nextDamageTime) return;
        if (!other.CompareTag("Enemy")) return;

        EnemyScript enemy = other.GetComponentInParent<EnemyScript>();
        if (enemy == null) return;

        nextDamageTime = Time.time + iFrames;
        TakeDamage(enemy.contactDamage);
    }

    void TakeDamage(float damage)
    {
        float finalDamage = Mathf.Max(damage - stats.armor, 0f);
        currentHP -= finalDamage;
        OnHealthChanged?.Invoke(currentHP, stats.maxHP);

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            OnHealthChanged?.Invoke(currentHP, stats.maxHP);
            isDead = true;
            OnGameOver?.Invoke();
        }
    }
}