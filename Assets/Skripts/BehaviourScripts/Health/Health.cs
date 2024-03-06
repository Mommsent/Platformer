using System;
using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Health : MonoBehaviour, IDamageable
{
    
    private Animator animator;
    
    private float InvincibilityTime = 0.25f;
    public bool IsInvincible { get; set; } = false;
    public int MaxHealth { get; set; } = 100;

    private bool isAlive;
    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool("IsAlive", value);
        }
    }


    [SerializeField] private int currentHealth = 100;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {   
            currentHealth = value;
            if (currentHealth <= 0)
            {
                IsAlive = false;
                Died?.Invoke();
            }
            Changed?.Invoke(MaxHealth, currentHealth);
        }
    }

    public Action<Vector2> Pushed;

    public Action<int,int> Changed;

    public Action Died;

    internal HealthTextSpawner healtTextSpawner;
    [Inject]
    private void Construct(HealthTextSpawner healtTextSpawner)
    {
        this.healtTextSpawner = healtTextSpawner;
    }
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

        Pushed?.Invoke(knockback);
    }
}
