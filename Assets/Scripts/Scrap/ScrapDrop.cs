using UnityEngine;

public class ScrapDrop : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int amount = 5;
    [SerializeField] private int score = 5;
    [SerializeField] private float activationTime = .25f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool canBePickedUp = false;
    private float spawnTime;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spawnTime = Time.time;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void FixedUpdate()
    {
        if (!canBePickedUp)
        {
            canBePickedUp = Time.time - spawnTime > activationTime;
        }
    }

    public void ApplyForce(Vector2 force)
    {
        rb.linearVelocity = force;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        CheckPlayerCollision(collision);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayerCollision(collision);
    }

    private void CheckPlayerCollision(Collider2D collision)
    {
        PlayerScrap playerScrap = collision.GetComponent<PlayerScrap>();

        if (playerScrap != null && canBePickedUp)
        {
            playerScrap?.Give(amount);
            GameManager.instance.AddScore(score);
            DestroyScrap();
        }
    }

    private void DestroyScrap()
    {
        Destroy(gameObject);
    }
}
