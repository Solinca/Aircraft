using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxLife;
    public float velocity;
    public int probability;
    public EnemyType type;

    private int currentLife;
    private GameObject player;

    private void Start()
    {
        currentLife = maxLife;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        transform.GetComponent<Rigidbody>().velocity = velocity * transform.forward;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            // Spawn explosion
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

        }
    }
}
