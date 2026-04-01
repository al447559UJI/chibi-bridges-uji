using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [Header("Data & Prefabs")]
    [SerializeField] private PlayerHealthData data;
    private SpriteRenderer spriteRenderer;


    private int currentHealth;
    private PlayerMovement movement;

    public UnityEvent<int> onHealthChanged;

    private bool isInvencible = false;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = data.maxHealth;
    }

    public void Damage(int damageAmount, DamageType damageType, int direction)
    {
        Debug.Log("PlayerHealth Damage");

        if (data.debugGodMode || isInvencible) return;

        currentHealth -= damageAmount;
        currentHealth = Math.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            movement.HurtKnockback(direction);
            StartCoroutine(InvencibleTime());
            StartCoroutine(Flicker());
        }

        onHealthChanged.Invoke(currentHealth);
    }

    public void Die()
    {
        Debug.Log("You died!");
    }


    private IEnumerator InvencibleTime()
    {
        isInvencible = true;
        yield return new WaitForSeconds(data.invencibleTime);
        isInvencible = false;
    }

    /// <summary>
    /// Handles the flickering effect of the sprite during the invincibility period.
    /// 
    /// After an initial delay (twice the flicker speed), the sprite visibility toggles 
    /// at intervals defined by <c>data.flickerSpeed</c>. This continues for the remaining flicker duration,
    /// calculated from the total invincibility time.
    /// </summary>
    private IEnumerator Flicker()
    {

        float waitTime = 2 * data.flickerSpeed;
        yield return new WaitForSeconds(waitTime);

        float timeLeft = GetFlickerTime() - waitTime;

        while (timeLeft > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(data.flickerSpeed);
            timeLeft -= data.flickerSpeed;
        }

        spriteRenderer.enabled = true;
    }

    private float GetFlickerTime()
    {
        return data.invencibleTime - data.invencibleTime * data.flickerPercentage / 100;
    }

}
