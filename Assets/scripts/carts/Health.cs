using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
