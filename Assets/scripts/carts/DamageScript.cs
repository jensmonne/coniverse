using UnityEngine;

public class DamageScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BumperCart"))
        {
            Damage(collision.gameObject);
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
