using UnityEngine;

public class PlayerParticles : MonoBehaviour
{

    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject jumpParticles;
    [SerializeField] GameObject landParticles;
    [SerializeField] Transform playerOrigin;
    [SerializeField] Transform playerFeet;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SpawnDeathParticles()
    {
        spriteRenderer.enabled = false;

        Vector3[] directions =
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

        foreach (Vector3 direction in directions)
        {
            GameObject newParticle = SpawnParticles(deathParticles, playerOrigin.position);
            newParticle.GetComponent<DeathParticle>()?.Initialize(direction);
        }
    }

    public void SpawnJumpParticles()
    {
        SpawnParticles(jumpParticles, playerFeet.position);
    }

    public void SpawnLandParticles()
    {
        SpawnParticles(landParticles, playerFeet.position);
    }

    private GameObject SpawnParticles(GameObject particles, Vector2 position)
    {
        return Instantiate(particles, position, Quaternion.identity);
    }
}
