using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody pickupRigidBody;
    private GameObject player;
    private PickupData data;

    private void Awake()
    {
        pickupRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    public void Initialize(PickupData pickupData, Vector3 spawnPosition)
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        transform.LookAt(player.transform);
        pickupRigidBody.velocity = pickupData.velocity * transform.forward;
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
        foreach (Bonus bonus in data.bonusList)
        {
            switch (bonus.type)
            {
                case UpgradeType.FLAT:
                    UpgradeManager.Instance.AddFlatTemporaryUpgrade(bonus.statType, bonus.value, data.lifetime);
                    break;
                case UpgradeType.PERCENTAGE:
                    UpgradeManager.Instance.AddPercentageTemporarytUpgrade(bonus.statType, bonus.value, data.lifetime);
                    break;
            }
        }
    }
}
