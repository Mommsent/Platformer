using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    Animator animator;

    Damageable damageable;

    public enum WalkableDirection {Right, Left};
    private WalkableDirection _walkDirection;

    private Vector2 walkableDirectionVector = Vector2.right;

    TouchingDirection _touchingDirection;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set 
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right) 
                {
                    walkableDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkableDirectionVector = Vector2.left;
                }    
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    private float walkStopeRate = 0.005f;

    public bool HasTarget 
    {
        get { return _hasTarget; }  
        private set 
        { 
            _hasTarget = value;
            animator.SetBool("HasTarget", value);
        } 
    }

    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
        private set { animator.SetBool("CanMove", value); }
    }

    public float AttackCooldown 
    { 
        get
        {
            return animator.GetFloat("AttackCooldown");
        }
        private set
        {
            animator.SetFloat("AttackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (AttackCooldown < 0)
            AttackCooldown = 0.5f;
    }

    private void FixedUpdate()
    {
        if(_touchingDirection.IsGrounded && _touchingDirection.IsOnWall)
        {
            FlipDirection();
        }

        if(!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(walkSpeed * walkableDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopeRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        damageable.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnNoGroundDetected()
    {
        if(_touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}