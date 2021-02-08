using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
public class PickupData : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public float probabilityOfSpawning;

    public BonusList[] bonusList;
    public float lifetime;
    public float velocity;
    public Pickup pickup;
}