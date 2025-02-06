using System.Collections;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    private float boostDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        SpeedBoost(collision.gameObject);
        Destroy(gameObject);
    }
    
    private void SpeedBoost(GameObject target)
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            StartCoroutine(SpeedBoostCoroutine(pm));
        }
    }
    
    private IEnumerator SpeedBoostCoroutine(PlayerMovement pm)
    {
        SpeedUp(pm);

        yield return new WaitForSeconds(boostDuration);

        ResetSpeed(pm);
    }

    private static void SpeedUp(PlayerMovement pm)
    {
        pm.acceleration += 20f;
        pm.maxSpeed += 15f;
        pm.rotationSpeed += 75f;
    }

    private static void ResetSpeed(PlayerMovement pm)
    {
        pm.acceleration = 20f;
        pm.maxSpeed = 10f;
        pm.rotationSpeed = 100f;
    }
}