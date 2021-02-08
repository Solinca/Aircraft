using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnFrequency;
    public float distanceFromThePlayer;
    public EnemyData[] enemiesData;

    private float cooldown = 0f;
    private EnemyType[] probabilities;
    private Renderer spawnRenderer;
    private Dictionary<EnemyType, EnemyData> typeToEnemyData = new Dictionary<EnemyType, EnemyData>();
    private Dictionary<EnemyType, List<Enemy>> pools = new Dictionary<EnemyType, List<Enemy>>();

    private void Start()
    {
        spawnRenderer = GetComponent<Renderer>();
        transform.position = new Vector3(0, 0, distanceFromThePlayer);
        BuildArrayOfProbabilities();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;

        if (cooldown >= spawnFrequency)
        {
            cooldown = 0f;
            SpawnEnemy();
        }
    }

    private void BuildArrayOfProbabilities()
    {
        float totalOfProbabilities = 0;

        foreach (EnemyData enemyData in enemiesData)
        {
            totalOfProbabilities += enemyData.probabilityOfSpawning;
        }

        probabilities = new EnemyType[(int) totalOfProbabilities];

        int count = 0;

        foreach (EnemyData enemyData in enemiesData)
        {
            BuildPoolOfEnnemy(enemyData, totalOfProbabilities);

            for (int i = 0; i < enemyData.probabilityOfSpawning; i++, count++)
            {
                probabilities[count] = enemyData.type;
            }
        }
    }

    private void BuildPoolOfEnnemy(EnemyData enemyData, float totalOfProbabilities)
    {
        pools[enemyData.type] = new List<Enemy>();
        typeToEnemyData[enemyData.type] = enemyData;

        for (int i = 0; i < (enemyData.probabilityOfSpawning / totalOfProbabilities) * 20 / spawnFrequency; i++)
        {
            CreateNewEnemy(enemyData);
        }
    }

    private void SpawnEnemy()
    {
        EnemyType randomEnemyType = probabilities[Random.Range(0, probabilities.Length)];
        GetEnemy(randomEnemyType).Initialize(typeToEnemyData[randomEnemyType], GetRandomSpawnPoint());
    }

    private Enemy GetEnemy(EnemyType enemyType)
    {
        for (int i = 0; i < pools[enemyType].Count; i++)
        {
            if (!pools[enemyType][i].gameObject.activeInHierarchy)
            {
                return pools[enemyType][i];
            }
        }

        return CreateNewEnemy(typeToEnemyData[enemyType]);
    }

    private Enemy CreateNewEnemy(EnemyData enemyData)
    {
        Enemy tempEnemy = Instantiate(enemyData.enemy.gameObject).GetComponent<Enemy>();
        pools[enemyData.type].Add(tempEnemy);

        return tempEnemy;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(spawnRenderer.bounds.min.x, spawnRenderer.bounds.max.x);
        float randomZ = Random.Range(spawnRenderer.bounds.min.z, spawnRenderer.bounds.max.z);

        return new Vector3(randomX, 0, randomZ);
    }
}
