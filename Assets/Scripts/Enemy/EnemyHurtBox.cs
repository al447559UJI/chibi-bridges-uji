using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    private EnemyController parent;

    void Awake()
    {
        parent = GetComponentInParent<EnemyController>();
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
