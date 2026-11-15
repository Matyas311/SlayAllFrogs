using System;
using UnityEngine;

public class playerLevel : MonoBehaviour
{

    public event Action<int, int> OnXPChanged; //current kills, needed
    public event Action<int> OnLevelUp;

    private int level = 1;
    public int Level => level;
    private int kills = 0;
    private int killsNeeded = 5;

    void OnEnable() => EnemyScript.OnEnemyDeath += HandleKill;
    void OnDisable() => EnemyScript.OnEnemyDeath -= HandleKill;

    void Start()
    {
        OnXPChanged?.Invoke(kills, killsNeeded);
    }
    void HandleKill()
    {
        kills++;
        if (kills >= killsNeeded)
        {
            kills = 0;
            level++;
            killsNeeded += 4;
            OnLevelUp?.Invoke(level);
        }
        OnXPChanged?.Invoke(kills, killsNeeded);
    }
}
