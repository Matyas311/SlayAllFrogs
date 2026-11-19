using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action<int> OnWaveChanged;

    [SerializeField] List<WaveTier> tiers;
    [SerializeField] List<MilestoneSpawn> milestones;
    [SerializeField] float spawnRadius = 40f;
    [SerializeField] float spawnInterval = 1.5f;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField, Range(0f, 0.5f)] float randomness = 0.4f;

    private int currentWave = 1;
    public int CurrentWave => currentWave;
    private int enemiesAlive = 0;

    void OnEnable() => EnemyScript.OnEnemyDeath += HandleEnemyDied;
    void OnDisable() => EnemyScript.OnEnemyDeath -= HandleEnemyDied;

    void Start()
    {
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (true)
        {
            OnWaveChanged?.Invoke(currentWave);

            WaveTier tier = GetTier(currentWave);
            if (tier != null)
            {
                float progress = tier.toWave > tier.fromWave
                    ? Mathf.InverseLerp(tier.fromWave, tier.toWave, currentWave)
                    : 0f;

                foreach (WaveEntry entry in tier.entries)
                {
                    int count = RollCount(entry, progress, tier.toWave - tier.fromWave + 1);
                    for (int i = 0; i < count; i++)
                    {
                        SpawnEnemy(entry.prefab);
                        yield return new WaitForSeconds(spawnInterval);
                    }
                }
            }

            foreach (MilestoneSpawn m in milestones)
            {
                if (m.everyNWaves > 0 && currentWave % m.everyNWaves == 0)
                {
                    for (int i = 0; i < m.count; i++)
                    {
                        SpawnEnemy(m.prefab);
                        yield return new WaitForSeconds(spawnInterval);
                    }
                }
            }

            yield return new WaitUntil(() => enemiesAlive <= 0);
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }
    }

    WaveTier GetTier(int wave)
    {
        foreach (WaveTier t in tiers)
            if (wave >= t.fromWave && wave <= t.toWave) return t;

        return tiers.Count > 0 ? tiers[tiers.Count - 1] : null;
    }

    int RollCount(WaveEntry entry, float progress, int tierLength)
    {
        float baseCount = Mathf.Lerp(entry.minCount, entry.maxCount, progress);
        float step = (entry.maxCount - entry.minCount) / Mathf.Max(1, tierLength - 1);
        float spread = step * randomness;
        int count = Mathf.RoundToInt(baseCount + UnityEngine.Random.Range(-spread, spread));
        return Mathf.Clamp(count, entry.minCount, entry.maxCount);
    }

    void SpawnEnemy(GameObject prefab)
    {
        float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(
            transform.position.x + spawnRadius * Mathf.Sin(angle),
            transform.position.y,
            transform.position.z + spawnRadius * Mathf.Cos(angle));

        Instantiate(prefab, pos, Quaternion.identity);
        enemiesAlive++;
    }

    void HandleEnemyDied()
    {
        enemiesAlive--;
    }
}