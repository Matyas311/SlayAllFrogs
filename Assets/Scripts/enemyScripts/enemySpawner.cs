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
                    int count = RollCount(entry, progress);
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

    int RollCount(WaveEntry entry, float progress)
    {
        float low = Mathf.Lerp(entry.minCount, entry.maxCount, progress * 0.5f);
        float high = Mathf.Lerp(entry.minCount, entry.maxCount, 0.5f + progress * 0.5f);
        return UnityEngine.Random.Range(Mathf.RoundToInt(low), Mathf.RoundToInt(high) + 1);
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