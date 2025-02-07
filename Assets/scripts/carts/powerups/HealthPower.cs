using UnityEngine;

public class HealthPower : PowerUp
{
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        HideObject();
        PlaySound();
        Heal(collision.gameObject);
        Destroy(gameObject, audioSource.clip.length);
        CollectPowerUp();
        Destroy(gameObject);
    }

    private void HideObject()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
    
    private static void Heal(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        hp.RegainHealth(1);
    }
}