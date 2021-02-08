using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance { get { return _instance; } }

    private static PickupManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    public PickupData[] pickupDataList;

    private string[] probabilities;
    private Dictionary<string, PickupData> nameToPickupData = new Dictionary<string, PickupData>();
    private Dictionary<string, List<Pickup>> pools = new Dictionary<string, List<Pickup>>();

    private void Start()
    {
        BuildArrayOfProbabilities();
    }

    private void BuildArrayOfProbabilities()
    {
        float totalOfProbabilities = 0;

        foreach (PickupData pickupData in pickupDataList)
        {
            totalOfProbabilities += pickupData.probabilityOfSpawning;
        }

        probabilities = new string[(int)totalOfProbabilities];

        int count = 0;

        foreach (PickupData pickupData in pickupDataList)
        {
            BuildPoolOfPickup(pickupData);

            for (int i = 0; i < pickupData.probabilityOfSpawning; i++, count++)
            {
                probabilities[count] = pickupData.name;
            }
        }
    }

    private void BuildPoolOfPickup(PickupData pickupData)
    {
        pools[pickupData.name] = new List<Pickup>();
        nameToPickupData[pickupData.name] = pickupData;

        CreateNewPickup(pickupData);
    }

    public void SpawnPickup(float probabilityOfPickupDrop, Vector3 spawnPoint)
    {
        if (Random.Range(0, 100) > probabilityOfPickupDrop)
        {
            return;
        }

        string randomPickupName = probabilities[Random.Range(0, probabilities.Length)];
        GetPickup(randomPickupName).Initialize(nameToPickupData[randomPickupName], spawnPoint);
    }

    private Pickup GetPickup(string pickupName)
    {
        for (int i = 0; i < pools[pickupName].Count; i++)
        {
            if (!pools[pickupName][i].gameObject.activeInHierarchy)
            {
                return pools[pickupName][i];
            }
        }

        return CreateNewPickup(nameToPickupData[pickupName]);
    }

    private Pickup CreateNewPickup(PickupData pickupData)
    {
        Pickup tempPickup = Instantiate(pickupData.pickup.gameObject).GetComponent<Pickup>();
        pools[pickupData.name].Add(tempPickup);

        return tempPickup;
    }
}
