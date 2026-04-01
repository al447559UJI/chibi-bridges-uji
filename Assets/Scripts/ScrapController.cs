using UnityEngine;
using UnityEngine.Events;

public class ScrapController : MonoBehaviour, IDamageable
{
    public UnityEvent scrapCollectEvent;

    void Start()
    {
        scrapCollectEvent.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().GiveScrap);
    }
    public void Damage(int damageAmount, DamageType damageType, int direction)
    {
        if (damageType == DamageType.MELEE)
        {
            scrapCollectEvent.Invoke();
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
