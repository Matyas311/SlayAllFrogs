using UnityEngine;
using System;
using UnityEngine.AI;
public class Life : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] float iFrames = 1f;

    private float nextDamageTime = 0f;
    private float currentHP;
    private float lastMaxHP;
    public event Action<float, float> OnHealthChanged; // current, max
    public float CurrentHP => currentHP;
    private bool isDead = false;

    public event Action OnGameOver;

    void OnEnable() => stats.OnStatsChanged += HandleStatsChanged;
    void OnDisable() => stats.OnStatsChanged -= HandleStatsChanged;

    void Start()
    {
        currentHP = stats.maxHP;
        lastMaxHP = stats.maxHP;
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

    void HandleStatsChanged()
    {
        float diff = stats.maxHP - lastMaxHP;
        if (diff == 0f) return;

        if (diff > 0f) currentHP += diff;
        currentHP = Mathf.Min(currentHP, stats.maxHP);
        lastMaxHP = stats.maxHP;

        OnHealthChanged?.Invoke(currentHP, stats.maxHP);

    }
}