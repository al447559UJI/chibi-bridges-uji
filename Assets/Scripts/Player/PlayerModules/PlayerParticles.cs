using UnityEngine;

public class PlayerParticles : MonoBehaviour
{

    [SerializeField] GameObject deathParticle;
    [SerializeField] Transform playerOrigin;

    private SpriteRenderer spriteRenderer;

    private static readonly Vector3[] directions =
    {
        new Vector3(0, 1, 0),
        new Vector3(1, 1, 0).normalized,
        new Vector3(1, 0, 0),
        new Vector3(1, -1, 0).normalized,
        new Vector3(0, -1, 0),
        new Vector3(-1, -1, 0).normalized,
        new Vector3(-1, 0, 0),
        new Vector3(-1, 1, 0).normalized
    };

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SpawnDeathParticles()
    {
        spriteRenderer.enabled = false;

        foreach (Vector3 direction in directions)
        {
            GameObject newParticle = Instantiate(deathParticle, playerOrigin.position, Quaternion.identity);
            newParticle.GetComponent<DeathParticle>()?.Initialize(direction);
        }
    }
}
