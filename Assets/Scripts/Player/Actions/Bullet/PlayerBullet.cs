using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    [SerializeField] private float lifetime = 3f; 
    [SerializeField] private GameObject hitParticle;
    private int damageAmount;
    private DamageType damageType;
    private float spawnTime;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.time - spawnTime > lifetime)
        {
            gameObject.SetActive(false);
        }
    }

    public void Initialize(float direction, float speed, int damage)
    {
        rb.linearVelocityX = direction * speed;
        damageAmount = damage;
        damageType = DamageType.BULLET;
        spawnTime = Time.time;

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageAmount, damageType, Math.Sign(rb.linearVelocityX));
        }

        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        spriteRenderer.flipX = false;
    }
}
