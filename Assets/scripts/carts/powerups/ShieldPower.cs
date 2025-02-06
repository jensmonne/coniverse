using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ShieldUp(collision.gameObject);
        Destroy(gameObject);
    }
    
    private static void ShieldUp(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        hp.RegainShield(1);
    }
}
