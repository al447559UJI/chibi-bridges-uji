using UnityEngine;

public class ScrapDrop : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void ApplyForce(Vector2 force)
    {
        rb.linearVelocity = force;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScrap playerScrap = collision.GetComponent<PlayerScrap>();

        if (playerScrap != null)
        {
            playerScrap?.Give(5);
            DestroyScrap();
        }
    }

    private void DestroyScrap()
    {
        Destroy(gameObject);
    }
}
