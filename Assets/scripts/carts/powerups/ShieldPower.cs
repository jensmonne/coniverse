using UnityEngine;

public class ShieldPower : PowerUp
{    
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        HideObject();
        PlaySound();
        ShieldUp(collision.gameObject);
        Destroy(gameObject, audioSource.clip.length);
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

    private void PlaySound()
    {
        audioSource.Play();
    }
    
    private static void ShieldUp(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        hp.RegainShield(1);
    }
}