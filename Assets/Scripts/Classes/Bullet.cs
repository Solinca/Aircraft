using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxLifetime;
    public int damage;
    public float velocity;

    private float lifetime = 0f;
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    public void Initialize(Transform spawn)
    {
        gameObject.SetActive(true);
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        bulletRigidbody.velocity = spawn.forward * UpgradeManager.Instance.GetFinalStat(UpgradeStatType.SPEED, velocity);
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
            collider.gameObject.GetComponent<Enemy>().TakeDamage((int) UpgradeManager.Instance.GetFinalStat(UpgradeStatType.DAMAGE, damage));
            Destruct();
        }
    }

    private void Destruct()
    {
        gameObject.SetActive(false);
    }
}
