using UnityEngine;

public class ScrapController : MonoBehaviour, IDamageable
{
    public void Damage(int damageAmount, DamageType damageType)
    {
        if (damageType == DamageType.MELEE)
        {
            Debug.Log("Give Player 50 scrap");
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
