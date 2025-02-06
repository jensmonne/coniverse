using System.Collections;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    private bool isBoosting = false;
    private float boostDuration = 5f;

    // ReSharper disable Unity.PerformanceAnalysis
    private void SpeedUp()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.acceleration += 20f;
        pm.maxSpeed += 15f;
        pm.rotationSpeed += 75f;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SpeedDown()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.acceleration -= 20f;
        pm.maxSpeed -= 15f;
        pm.rotationSpeed -= 75f;
    }

    public void SpeedBoost()
    {
        if (!isBoosting)
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
    }
    
    private IEnumerator SpeedBoostCoroutine()
    {
        SpeedUp();

        yield return new WaitForSeconds(boostDuration);

        SpeedDown();
    }
}
