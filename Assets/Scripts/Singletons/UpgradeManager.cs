using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get { return _instance; } }

    private static UpgradeManager _instance;

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

    private Dictionary<UpgradeStatType, float> percentagePermanentUpgrades = new Dictionary<UpgradeStatType, float>();
    private Dictionary<UpgradeStatType, float> percentageTemporaryUpgrades = new Dictionary<UpgradeStatType, float>();
    private Dictionary<UpgradeStatType, float> flatPermanentUpgrades = new Dictionary<UpgradeStatType, float>();
    private Dictionary<UpgradeStatType, float> flatTemporaryUpgrades = new Dictionary<UpgradeStatType, float>();

    public float GetFinalStat(UpgradeStatType type, float baseStat)
    {
        float flatPermanentValue = flatPermanentUpgrades.ContainsKey(type) ? flatPermanentUpgrades[type] : 0;
        float percentagePermanentValue = percentagePermanentUpgrades.ContainsKey(type) ? percentagePermanentUpgrades[type] : 0;
        float flatTemporaryValue = flatTemporaryUpgrades.ContainsKey(type) ? flatTemporaryUpgrades[type] : 0;
        float percentageTemporaryValue = percentageTemporaryUpgrades.ContainsKey(type) ? percentageTemporaryUpgrades[type] : 0;

        return (baseStat + flatPermanentValue + flatTemporaryValue) * (1 - percentagePermanentValue / 100) * (1 - percentageTemporaryValue / 100);
    }

    public void AddFlatPermanentUpgrade(UpgradeStatType type, float value)
    {
        flatPermanentUpgrades[type] = flatPermanentUpgrades.ContainsKey(type) ? flatPermanentUpgrades[type] + value : value;
    }

    public void AddPercentagePermanentUpgrade(UpgradeStatType type, float value)
    {
        percentagePermanentUpgrades[type] = percentagePermanentUpgrades.ContainsKey(type) ? percentagePermanentUpgrades[type] + value : value;
    }

    public void AddFlatTemporaryUpgrade(UpgradeStatType type, float value, float lifetime)
    {
        flatTemporaryUpgrades[type] = flatTemporaryUpgrades.ContainsKey(type) ? flatTemporaryUpgrades[type] + value : value;

        StartCoroutine(WaitForTemporaryBoost(lifetime, flatTemporaryUpgrades, type, value));
    }

    public void AddPercentageTemporarytUpgrade(UpgradeStatType type, float value, float lifetime)
    {
        percentageTemporaryUpgrades[type] = percentageTemporaryUpgrades.ContainsKey(type) ? percentageTemporaryUpgrades[type] + value : value;

        StartCoroutine(WaitForTemporaryBoost(lifetime, percentageTemporaryUpgrades, type, value));
    }

    private IEnumerator WaitForTemporaryBoost(float lifetime, Dictionary<UpgradeStatType, float>  dictionnary, UpgradeStatType type, float value)
    {
        yield return new WaitForSeconds(lifetime);

        dictionnary[type] -= value;
    }
}
