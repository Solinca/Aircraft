using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxLifetime;
    public int damage;
    public float velocity;

    private float lifetime = 0f;

    public void Initialize(Transform weapon)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = weapon.position;
        gameObject.transform.rotation = weapon.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = weapon.forward * velocity;
        lifetime = 0f;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime > maxLifetime)
        {
            Destruct();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destruct();
        }
    }

    private void Destruct()
    {
        gameObject.SetActive(false);
    }
}
