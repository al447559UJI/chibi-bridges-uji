using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeHitbox : MonoBehaviour
{
    private BoxCollider2D hitbox;

    private HashSet<IDamageable> hitDamageables;

    void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>(); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect Enemy
    }
}
