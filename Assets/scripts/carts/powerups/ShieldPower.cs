using UnityEngine;

public class ShieldPower : MonoBehaviour
{    
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        PlaySound();
        ShieldUp(collision.gameObject);
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
    
    private static void ShieldUp(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        hp.RegainShield(1);
    }
}