using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float frequency;
    public Enemy[] enemies;

    private float cooldown = 0f;
    private EnemyType[] probabilities;
    private Dictionary<EnemyType, Enemy> enemyTypes = new Dictionary<EnemyType, Enemy>();

    private void Start()
    {
        int arrayLength = 0;

        foreach (Enemy enemy in enemies)
        {
            arrayLength += enemy.probability;
        }

        probabilities = new EnemyType[arrayLength];

        int count = 0;

        foreach (Enemy enemy in enemies)
        {
            enemyTypes[enemy.type] = enemy;

            for (int i = 0; i < enemy.probability; i++, count++)
            {
                probabilities[count] = enemy.type;
            }
        }
    }

    private void Update()
    {
        cooldown += Time.deltaTime;

        if (cooldown >= frequency)
        {
            cooldown = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int random = Random.Range(0, probabilities.Length);
        EnemyType randomEnemy = probabilities[random];
        Instantiate(enemyTypes[randomEnemy], new Vector3(0, 0, 200), Quaternion.identity);
    }
}
