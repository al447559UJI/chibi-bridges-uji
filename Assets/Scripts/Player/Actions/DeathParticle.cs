using System.Collections;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float destroyTime = 2f;

    [SerializeField] private Sprite[] frames;
    [SerializeField] private float fps = 10f;

    private float timer;
    private int currentFrame;
    private SpriteRenderer spriteRenderer;

    public void Initialize(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        StartCoroutine(DestroyParticle());
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Animate();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void Animate()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;

            spriteRenderer.sprite = frames[currentFrame];
        }
    }

}
