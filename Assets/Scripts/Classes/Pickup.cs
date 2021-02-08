using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody enemyRigidbody;
    private GameObject player;
    private PickupData data;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    public void Initialize(PickupData pickupData, Vector3 spawnPosition)
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        transform.LookAt(player.transform);
        enemyRigidbody.velocity = pickupData.velocity * transform.forward;
        data = pickupData;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ApplyBonuses();
            Destruct();
        }
    }

    public void Destruct()
    {
        gameObject.SetActive(false);
    }

    private void ApplyBonuses()
    {
        foreach (BonusList bonus in data.bonusList)
        {
            switch (bonus.type)
            {
                case UpgradeType.FLAT:
                    UpgradeManager.Instance.AddFlatTemporaryUpgrades(bonus.statType, bonus.value, data.lifetime);
                    break;
                case UpgradeType.PERCENTAGE:
                    UpgradeManager.Instance.AddPercentageTemporarytUpgrades(bonus.statType, bonus.value, data.lifetime);
                    break;
            }
        }
    }
}
