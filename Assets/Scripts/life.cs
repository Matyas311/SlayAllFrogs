using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] float maxHP = 100f;
    [SerializeField] float armor = 0f;
    [SerializeField] float hpRegen = 1.2f; //out of combat (not taking damage or shooting) regenerate this amout per second
    [SerializeField] float iFrames = 1f; //time before taking damage again 
    private float nextDamageTime = 0f;
    private float currentHP;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
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
        float finalDamage = Mathf.Max(damage - armor, 0f);
        currentHP -= finalDamage;
        Debug.Log(currentHP);

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            isDead = true;
            Debug.Log("Game over");
        }
    }
}
