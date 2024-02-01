using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float airMoveSpeed = 3f;
    Damageable damagable;

    public float CurrentMoveSpeed
    {
        get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                            return runSpeed;

                        return walkSpeed;
                    }
                    else
                    {
                        return airMoveSpeed;
                    }
                }
                return 0;
            }
            return 0;
        }
    }

    private Vector2 moveInput;
    Rigidbody2D rb;
    private bool _isMoving = false;
    private bool _isRunning = false;
    private Animator animator;
    TouchingDirection touchingDirections;

    public bool CanMove
    {
        get 
        {
            return animator.GetBool("CanMove");
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool("IsAlive");
        }
    }

    public bool IsMoving { get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", _isMoving);
        }
    }

    public bool IsRunning
    {
        get 
        {
            return _isRunning; 
        }
        set 
        { 
            _isRunning = value;
            animator.SetBool("IsRunning", _isRunning);
        }
    }

    public bool _isFacingRight = true;
    private float jumpImpulse = 10f;

    public bool IsFacingRight 
    { get
        {
            return _isFacingRight;
        }
        private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if(!damagable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive && !damagable.LockVelocity)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDerection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDerection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight= false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if alive as well
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("RangedAttack");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        damagable.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
