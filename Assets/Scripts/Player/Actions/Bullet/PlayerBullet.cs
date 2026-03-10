using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    [SerializeField] private float lifetime = 3f; 
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

    public void Initialize(float direction, float speed)
    {
        rb.linearVelocityX = direction * speed;
        spawnTime = Time.time;

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        spriteRenderer.flipX = false;
    }
}
