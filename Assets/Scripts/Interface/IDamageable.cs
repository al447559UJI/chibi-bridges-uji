using UnityEngine;

public interface IDamageable
{
    public void Damage(int damageAmount, DamageType damageType, int direction);
    public void Die();
}
