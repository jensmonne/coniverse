using UnityEngine;

public class KnockbackPower : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        KnockbackIncrease(collision.gameObject);
        Destroy(gameObject);
    }
    
    private static void KnockbackIncrease(GameObject target)
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        pm.bumpForce += 600f;
    }
}