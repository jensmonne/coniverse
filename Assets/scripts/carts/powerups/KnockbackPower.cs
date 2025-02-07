using UnityEngine;

public class KnockbackPower : PowerUp
{
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        HideObject();
        PlaySound();
        KnockbackIncrease(collision.gameObject);
        CollectPowerUp();
        Destroy(gameObject);
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

    private static void KnockbackIncrease(GameObject target)
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        if (pm != null) pm.bumpForce += 600f;
    }
}