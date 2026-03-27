using UnityEngine;

public interface IDamageable
{
    public void Damage(int damageAmount, DamageType damageType);
    public void Die();
}
