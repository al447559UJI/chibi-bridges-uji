using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    private IHurtBoxUser parent;

    void Awake()
    {
        parent = GetComponentInParent<IHurtBoxUser>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            parent.MeleeAttack(damageable);
        }
    }
}
