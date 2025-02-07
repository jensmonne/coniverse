using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform[] spawnPoints; // Assign 4 empty GameObjects as spawn points in the Inspector

    private void Start()
    {
        SpawnPowerUps();
    }

    private void SpawnPowerUps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}