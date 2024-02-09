using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public HealthTextSpawner healtTextSpawner;
    private float InvincibilityTime = 0.25f;
    private Animator animator;
    public bool IsInvincible { get; set; } = false;
    public int MaxHealth { get; set; } = 100;

    private bool isAlive;
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
            animator.SetBool("IsAlive", value);
        }
    }


    [SerializeField] private int currentHealth = 100;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {   
            currentHealth = value;
            if (currentHealth <= 0)
            {
                IsAlive = false;
                Died?.Invoke();
            }
            Changed?.Invoke(currentHealth, MaxHealth);
        }

    }

    public Action<Vector2> Pushed;

    public Action<int, int> Changed;

    public Action Died;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        IsAlive = true;
        IsInvincible = false;
    }

    public IEnumerator invincibleTime(float time)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }

    public virtual bool IsCanBeReduced()
    {
        if(IsAlive && !IsInvincible) 
        {
            return true;
        }
        return false;
    }

    public virtual void Reduce(int damage, Vector2 knockback)
    {
        CurrentHealth -= damage;

        animator.SetTrigger("Hit");
        StartCoroutine(invincibleTime(InvincibilityTime));

        healtTextSpawner.CharacterTookDamage(this, damage);

        Changed?.Invoke(MaxHealth, CurrentHealth);
        Pushed?.Invoke(knockback);
    }
}
