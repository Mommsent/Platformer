using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public Action<Health, int> Restored;
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

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("LockVelocity");
        }
        set
        {
            animator.SetBool("LockVelocity", value);
        }
    }

    private int currentHealth = 100;
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

    public Action<Health, int> gotDamage;
    
    //public delegate void GotHit(int damage, Vector2 knockback);
    //public event GotHit gotHit;

    public Action<int, int> Changed;
    //public delegate void HealtChanged(int maxHealth, int newValue);
    //public event HealtChanged healtChanged;

    public Action Died;
    //public delegate void Died();
    //public event Died died;

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

    public bool Reduce(int damage, Vector2 knockback)
    {
        if (IsAlive && !IsInvincible)
        {
            CurrentHealth -= damage;
            animator.SetTrigger("Hit");
            LockVelocity = true;
            gotDamage?.Invoke(this, damage);
            Pushed?.Invoke(knockback);
            StartCoroutine(invincibleTime(InvincibilityTime));
            return true;
        }

        return false;
    }

    public bool IsRestored(int healthRestore)
    {
        if (IsAlive)
        {
            CurrentHealth += healthRestore;
            Restored.Invoke(this, healthRestore);
            return true;
        }
        return false;
    }
}
