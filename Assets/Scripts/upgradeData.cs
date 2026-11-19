using System;

public enum StatType { Damage, FireRate, MoveSpeed, ProjectileSpeed, Armor, HpRegen, maxHP }

[Serializable]
public class UpgradeData
{
    public string title;
    public string description;
    public StatType stat;
    public float amount;
}