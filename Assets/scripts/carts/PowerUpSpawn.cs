using UnityEngine;
using System.Collections;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs; // Different power-up types
    [SerializeField] private Transform[] spawnPoints; // Possible spawn locations

    private GameObject[] activePowerUps; // Tracks currently active power-ups

    private void Start()
    {
        activePowerUps = new GameObject[spawnPoints.Length]; 
        SpawnInitialPowerUps();
    }

    private void SpawnInitialPowerUps()
    {
        for (int i = 0; i < 4; i++) // Ensure only 4 power-ups spawn at start
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
        } while (activePowerUps[randomIndex] != null); // Ensure an empty spawn point

        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)]; // Pick a random power-up
        GameObject spawnedPowerUp = Instantiate(powerUpPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        
        activePowerUps[randomIndex] = spawnedPowerUp;

        PowerUp powerUpScript = spawnedPowerUp.GetComponent<PowerUp>();
        if (powerUpScript != null)
        {
            powerUpScript.SetSpawner(this, randomIndex); // Allow the power-up to notify the spawner when picked up
        }
    }

    public void PowerUpCollected(int spawnIndex)
    {
        activePowerUps[spawnIndex] = null; // Clear the slot
        StartCoroutine(RespawnPowerUpAfterDelay(spawnIndex, 20f));
    }

    private IEnumerator RespawnPowerUpAfterDelay(int spawnIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnPowerUp();
    }
}