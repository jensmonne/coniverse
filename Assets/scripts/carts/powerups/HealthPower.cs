using UnityEngine;

public class HealthPower : MonoBehaviour
{
    public void Heal()
    {
        Health health = gameObject.GetComponent<Health>();
        health.RegainHealth(1);
    }
}
