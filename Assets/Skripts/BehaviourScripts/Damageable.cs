using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    public UnityEvent Died;

    [SerializeField] private int _maxHealth = 100;
    Animator _animator;
    public int MaxHealth
    {
        get 
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged.Invoke(_health, _maxHealth);
            if (_health <= 0) 
            {
                IsAlive = false;
                Died.Invoke();
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;

    [SerializeField] private float timeSinceHit = 0;
    [SerializeField] private float InvincibilityTime = 0.25f;

    public bool IsAlive 
    { 
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            _animator.SetBool("IsAlive", value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return _animator.GetBool("LockVelocity");
        }
        set
        {
            _animator.SetBool("LockVelocity", value);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        IsAlive = true;
    }

    public void Update() 
    {
        if(isInvincible)
        {
            if (timeSinceHit > InvincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    
    public bool Hit(int damage, Vector2 knockback)
    {
        if(_isAlive && !isInvincible) 
        {
            Health -= damage;
            isInvincible = true;

            _animator.SetTrigger("Hit");
            LockVelocity = true;
            //damageableHit?.Invoke(damage, knockback);
            //CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            //CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
