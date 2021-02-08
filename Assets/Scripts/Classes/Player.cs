using UnityEngine;

public class Player : MonoBehaviour, IAircraft
{
    public int maxLife;

    private int currentLife;

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        // Implement GameOver
    }
}
