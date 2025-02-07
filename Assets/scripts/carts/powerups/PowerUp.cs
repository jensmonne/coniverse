using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PowerUpSpawn spawner;
    private int spawnIndex;

    public void SetSpawner(PowerUpSpawn spawner, int index)
    {
        this.spawner = spawner;
        this.spawnIndex = index;
    }

    protected void CollectPowerUp()
    {
        if (spawner != null)
        {
            spawner.PowerUpCollected(spawnIndex); // Notify the spawner
        }
        Destroy(gameObject); // Destroy after applying effect
    }
}