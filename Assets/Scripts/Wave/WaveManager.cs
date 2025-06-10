using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public EnemySpawner[] spawners;

    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject bossEnemyPrefab;

    public int enemiesPerWave = 3;
    public float timeBetweenWaves = 5f;
    public float spawnDelay = 0.5f;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new();
    private int enemiesRemaining = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        Debug.Log($"Wave {currentWave} 시작");
        UIManager.Instance.UpdateWaveUI(currentWave);

        int enemiesSpawned = 0;
        while (enemiesSpawned < enemiesPerWave)
        {
            EnemySpawner spawner = spawners[Random.Range(0, spawners.Length)];

            (GameObject prefab, EnemyType type) = GetEnemyPrefabByWave(currentWave);
            GameObject enemy = Instantiate(prefab, spawner.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(currentWave, type);

            activeEnemies.Add(enemy);
            enemiesSpawned++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void Update()
    {
        activeEnemies.RemoveAll(e => e == null);

        if (activeEnemies.Count == 0 && !IsInvoking(nameof(InvokeWave)))
        {
            Invoke(nameof(InvokeWave), timeBetweenWaves);
        }
    }

    private void InvokeWave()
    {
        StartCoroutine(StartNextWave());
    }

    public void StartWaveExternally()
    {
        StartCoroutine(StartNextWave());
    }

    private (GameObject, EnemyType) GetEnemyPrefabByWave(int wave)
    {
        if (wave % 10 == 0)
            return (bossEnemyPrefab, EnemyType.Boss);
        else if (wave % 5 == 0)
            return (tankEnemyPrefab, EnemyType.Tank);
        else
        {
            int random = Random.Range(0, 3);
            return random switch
            {
                0 => (normalEnemyPrefab, EnemyType.Normal),
                1 => (fastEnemyPrefab, EnemyType.Fast),
                2 => (rangedEnemyPrefab, EnemyType.Ranged),
                _ => (normalEnemyPrefab, EnemyType.Normal)
            };
        }
    }
}
