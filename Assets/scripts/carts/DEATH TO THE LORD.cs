using UnityEngine;

public class DEATHTOTHELORD : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(42);
            Debug.Log("DEATH");
        }
    }
}
