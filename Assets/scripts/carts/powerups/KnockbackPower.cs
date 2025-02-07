using UnityEngine;

public class KnockbackPower : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        PlaySound();
        KnockbackIncrease(collision.gameObject);
        HideObject();
        Destroy(gameObject, audioSource.clip.length);
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

    private static void KnockbackIncrease(GameObject target)
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        pm.bumpForce += 600f;
    }
}