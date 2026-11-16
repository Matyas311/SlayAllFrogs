using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] Life life;
    [SerializeField] playerLevel level;
    [SerializeField] EnemySpawner wave;

    [Header("Stat texts")]
    [SerializeField] TMP_Text moveSpeedText;
    [SerializeField] TMP_Text regenText;
    [SerializeField] TMP_Text projectileSpeedText;
    [SerializeField] TMP_Text fireRateText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text armorText;

    [Header("Health bar")]
    [SerializeField] Image healthFill;
    [SerializeField] TMP_Text healthText;

    [Header("XP bar")]
    [SerializeField] Image XPFill;
    [SerializeField] TMP_Text XPText;

    [Header("Waves")]
    [SerializeField] TMP_Text waveNumber;

    void OnEnable()
    {
        stats.OnStatsChanged += RefreshStats;
        life.OnHealthChanged += RefreshHealth;
        level.OnXPChanged += RefreshLevel;
        wave.OnWaveChanged += RefreshWaves;
    }

    void OnDisable()
    {
        stats.OnStatsChanged -= RefreshStats;
        life.OnHealthChanged -= RefreshHealth;
        level.OnXPChanged -= RefreshLevel;
        wave.OnWaveChanged -= RefreshWaves;
    }

    void Start()
    {
        RefreshStats();
    }

    void RefreshStats()
    {
        moveSpeedText.text = stats.moveSpeed.ToString("0.#");
        regenText.text = stats.hpRegen.ToString("0.#");
        projectileSpeedText.text = stats.hpRegen.ToString("0.#");
        fireRateText.text = stats.hpRegen.ToString("0.#");
        damageText.text = stats.damage.ToString("0.#");
        armorText.text = stats.armor.ToString("0.#");
    }

    void RefreshHealth(float current, float max)
    {
        healthFill.fillAmount = current / max;
        healthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }

    void RefreshLevel(int kills, int killsNeeded)
    {
        XPFill.fillAmount = (float)kills / killsNeeded;
        XPText.text = $"LVL: {level.Level}";
    }

    void RefreshWaves(int currentWave)
    {
        waveNumber.text = wave.CurrentWave.ToString();
    }

}