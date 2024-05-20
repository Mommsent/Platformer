using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour, IDamageable
{
    
    private Animator animator;
    
    private float InvincibilityTime = 0.25f;
    public bool IsInvincible { get; set; } = false;

    [SerializeField] private int maxHealth = 100;
    public int MaxHealth 
    {
        get{ return maxHealth; }
        set { maxHealth = value; }
    }

    private bool isAlive = true;
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
        if(currentHealth < 0) 
        {
            currentHealth = 0;
        }

        animator.SetTrigger("Hit");
        StartCoroutine(invincibleTime(InvincibilityTime));

        healtTextSpawner.CharacterTookDamage(this, damage);

        Pushed?.Invoke(knockback);
    }

    public IEnumerator invincibleTime(float time)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }
}
