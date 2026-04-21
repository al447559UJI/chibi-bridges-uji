using UnityEngine;

public class KillParticles : MonoBehaviour
{

    [SerializeField] private GameObject particle;
    [SerializeField] private float minOffsetInclusive;
    [SerializeField] private float maxOffsetIExclusive;
    private bool behaviorFinished = false;

    void Update()
    {
        if (behaviorFinished)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnParticles();
    }

    private void SpawnParticles()
    {
        int particleAmount = Random.Range(2, 6);
        Vector3 origin = transform.position;

        for (int i = 0; i < particleAmount; i++)
        {
            float offsetX = ApplyOffset(origin.x, minOffsetInclusive, maxOffsetIExclusive);
            float offsetY = ApplyOffset(origin.y, minOffsetInclusive, maxOffsetIExclusive);

            Vector3 particleOrigin = new Vector3(offsetX, offsetY, origin.z);

            Instantiate(particle, particleOrigin, Quaternion.identity);
        }

        behaviorFinished = true;
    }

    private float ApplyOffset(float number, float minInclusive, float maxExclusive)
    {
        return number += Random.Range(minInclusive, maxExclusive);
    }
}
