using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public int probabilityOfSpawn;

    [Range(0.0f, 100.0f)]
    public int probabilityOfPickupDrop;

    public int maxLife;
    public float velocity;
    public int damage;
    public EnemyType type;
    public Enemy enemy;
}