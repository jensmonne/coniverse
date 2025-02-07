using UnityEngine;

public class DamageScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BumperCart"))
        {
            Damage(gameObject);
        }
    }

    private static void Damage(GameObject cart)
    {
        Health cartHealth = cart.GetComponent<Health>();
        if (cartHealth != null)
        {
            cartHealth.TakeDamage(1);
        }
    }
}
