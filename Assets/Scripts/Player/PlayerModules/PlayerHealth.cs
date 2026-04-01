using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [Header("Data & Prefabs")]
    [SerializeField] private PlayerHealthData data;

    private int currentHealth;
    private PlayerMovement movement;

    public UnityEvent<int> onHealthChanged;

    private bool isInvencible = false;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
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

}
