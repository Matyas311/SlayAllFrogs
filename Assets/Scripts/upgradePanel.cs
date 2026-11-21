using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgradePanel : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] playerLevel playerLevel;
    [SerializeField] GameObject panel;
    [SerializeField] List<UpgradeData> pool;
    [SerializeField] Button[] buttons;
    [SerializeField] TMP_Text[] titles;
    [SerializeField] TMP_Text[] descriptions;


    private UpgradeData[] offered = new UpgradeData[2];

    void OnEnable() => playerLevel.OnLevelUp += Show;
    void OnDisable() => playerLevel.OnLevelUp -= Show;

    void Start()
    {
        panel.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => Choose(index));
        }
    }

    void Show(int level)
    {
        int first = Random.Range(0, pool.Count);
        int second;
        do { second = Random.Range(0, pool.Count); } while (second == first);

        offered[0] = pool[first];
        offered[1] = pool[second];

        for (int i = 0; i < 2; i++)
        {
            titles[i].text = offered[i].title;
            descriptions[i].text = offered[i].description;
        }

        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Choose(int index)
    {
        Apply(offered[index]);
        stats.NotifyChanged();
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Apply(UpgradeData u)
    {
        switch (u.stat)
        {
            case StatType.Damage: stats.damage += u.amount; break;
            case StatType.FireRate: stats.fireRate = Mathf.Max(0.1f, stats.fireRate + u.amount); break;
            case StatType.MoveSpeed: stats.moveSpeed += u.amount; break;
            case StatType.ProjectileSpeed: stats.projectileSpeed += u.amount; break;
            case StatType.Armor: stats.armor += u.amount; break;
            case StatType.MaxHP: stats.maxHP += u.amount; break;
        }
    }

}
