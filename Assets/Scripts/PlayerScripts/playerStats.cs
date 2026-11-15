using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public event Action OnStatsChanged;

    public float moveSpeed = 15.0f;
    public float hpRegen = 0.8f;
    public float damage = 5.0f;
    public float fireRate = 1.0f;
    public float projectileSpeed = 20.0f;
    public float armor = 0.0f;
    public float maxHP = 100.0f;



    public void NotifyChanged() => OnStatsChanged?.Invoke();
}