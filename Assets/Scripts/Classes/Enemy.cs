using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IAircraft
{
    public GameObject explosion;
    public GameObject body;

    private int currentLife;
    private SphereCollider sphereCollider;
    private Rigidbody enemyRigidbody;
    private GameObject player;
    private EnemyData data;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    public void Initialize(EnemyData enemyData, Vector3 spawnPosition)
    {
        gameObject.SetActive(true);
        sphereCollider.enabled = true;
        body.SetActive(true);
        transform.position = spawnPosition;
        transform.LookAt(player.transform);
        enemyRigidbody.velocity = enemyData.velocity * transform.forward;
        currentLife = enemyData.maxLife;
        data = enemyData;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Destruct();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(data.damage);
            Destruct();
        }
    }

    public void Destruct()
    {
        PickupManager.Instance.SpawnPickup(data.probabilityOfPickupDrop, transform.position);
        sphereCollider.enabled = false;
        body.SetActive(false);

        StartCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        explosion.SetActive(true);

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
        explosion.SetActive(false);
    }
}
