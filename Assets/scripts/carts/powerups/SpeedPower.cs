using System.Collections;
using UnityEngine;

public class SpeedPower : PowerUp
{
    
    [SerializeField] private AudioSource audioSource;

    private float boostDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        HideObject();
        SpeedBoost(collision.gameObject);
        PlaySound();
        CollectPowerUp();
    }
    
    private void HideObject()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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
        Destroy(gameObject);
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

    private void PlaySound()
    {
        audioSource.Play();
    }
}