using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Настройки для первого типа врагов")]
    public GameObject enemyType1Prefab;
    public Transform[] spawnPointsType1;
    public float spawnIntervalType1 = 1.5f;
    public int totalEnemiesType1 = 7;

    [Header("Настройки для второго типа врагов")]
    public GameObject enemyType2Prefab;
    public Transform[] spawnPointsType2;
    public float spawnIntervalType2 = 2.0f;
    public int totalEnemiesType2 = 8;

    private int enemiesSpawnedType1 = 0;
    private int enemiesSpawnedType2 = 0;

    private int totalEnemiesToSpawn;

    private void Start()
    {
        totalEnemiesToSpawn = totalEnemiesType1 + totalEnemiesType2;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.totalEnemiesToSpawn = totalEnemiesToSpawn;

        StartCoroutine(SpawnAllEnemies());
    }

    private IEnumerator SpawnAllEnemies()
    {
        float timer1 = 0f;
        float timer2 = 0f;

        while (enemiesSpawnedType1 < totalEnemiesType1 || enemiesSpawnedType2 < totalEnemiesType2)
        {
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;

            // Спавним первого типа врагов с интервалом spawnIntervalType1
            if (enemiesSpawnedType1 < totalEnemiesType1 && timer1 >= spawnIntervalType1)
            {
                SpawnEnemy(enemyType1Prefab, spawnPointsType1);
                enemiesSpawnedType1++;
                timer1 = 0f;
            }

            // Спавним второго типа врагов с интервалом spawnIntervalType2
            if (enemiesSpawnedType2 < totalEnemiesType2 && timer2 >= spawnIntervalType2)
            {
                SpawnEnemy(enemyType2Prefab, spawnPointsType2);
                enemiesSpawnedType2++;
                timer2 = 0f;
            }

            yield return null; 
        }

        // Все враги заспавнены — сообщаем ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SetAllEnemiesSpawned();
        }
    }

    private void SpawnEnemy(GameObject prefab, Transform[] spawnPoints)
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
