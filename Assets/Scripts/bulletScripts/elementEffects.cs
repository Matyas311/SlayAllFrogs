using System.Runtime.InteropServices;
using UnityEngine;

public static class ElementEffects
{
    const float WaterRadius = 4f;
    const float WaterDamageMultiplier = 0.5f;

    public static void OnHit(Element element, EnemyScript primary, Vector3 point, float damage, Vector3 hitDirection)
    {
        switch (element)
        {
            case Element.Water:
                Splash(primary, point, damage * WaterDamageMultiplier, WaterRadius);
                break;
            case Element.Fire:
                primary.ApplyBurn(2, 0.05f);
                break;
            case Element.Ground:
                primary.ApplySlow(2f, 0.7f);
                break;
            case Element.Wind:
                primary.ApplyKnockback(hitDirection, 18f);
                break;
        }
    }

    static void Splash(EnemyScript primary, Vector3 center, float damage, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(center, radius, ~0, QueryTriggerInteraction.Collide);
        var alreadyHit = new System.Collections.Generic.HashSet<EnemyScript>();

        foreach (Collider c in hits)
        {
            EnemyScript enemy = c.GetComponentInParent<EnemyScript>();
            if (enemy == null || enemy == primary) continue;
            if (!alreadyHit.Add(enemy)) continue;

            enemy.TakeDamage(damage);
        }
    }

}
