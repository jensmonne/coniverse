using UnityEngine;
using System.Collections;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 17f;

    private void Start()
    {
        StartCoroutine(SpawnPowerUpsRoutine());
    }

    private IEnumerator SpawnPowerUpsRoutine()
    {
        while (true)
        {
            SpawnPowerUps();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPowerUps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Believe me this i think works like i intended it to. Simon is killing himself in the background while typing this!
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpInstance = Instantiate(powerUpPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
            powerUpInstance.transform.SetParent(spawnPoint);
        }
    }
}