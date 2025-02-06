using UnityEngine;

public class HealthPower : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Heal(collision.gameObject);
        Destroy(gameObject);
    }

    private static void Heal(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        hp.RegainHealth(1);
    }
}
