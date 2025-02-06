using UnityEngine;

public class KnockbackPower : MonoBehaviour
{
    public void RemoveKnockback()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.bumpForce = 0f;
    }

    public void KnockbackUp()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.bumpForce += 600f;
    }
}
