using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isMoving = false;
    private bool isRunning = false;
    private Animator animator;
    private TouchingDirection touchingDirections;

    private StateMachine movementSM;
    public StandingState standingState;
    public WalkingState walkingState;
    public JumpState jumpState;
    public AirState fallingState;

    [SerializeField] ProjectileSpawner projectileSpawner;

    public PlayerHealth Health { get; set; }

    private float currentSpeed;
    public float CurrentMoveSpeed
    {
        get
        {
            return currentSpeed;
        }
        set
        {
            currentSpeed = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return touchingDirections.IsGrounded;
        }
    }

    public bool IsOnWall
    {
        get
        {
            return touchingDirections.IsOnWall;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("CanMove");
        }
        set
        {
            animator.SetBool("CanMove", value);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool("IsAlive");
        }
    }

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        set
        {
            isMoving = value;
            animator.SetBool("IsMoving", isMoving);
        }
    }

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            isRunning = value;
            animator.SetBool("IsRunning", isRunning);
        }
    }

    public bool IsJumping { get;  set; } 

    private bool isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if (isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            isFacingRight = value;
        }
    }

    public IAmmo Ammo { get; set; }

    [Inject]
    public void Construct(IAmmo ammo)
    {
        this.Ammo = ammo;
    }

    private void OnEnable()
    {
        Health.Pushed += OnHit;
    }

    private void Awake()
    {
        Health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        movementSM = new StateMachine();

        standingState = new StandingState(this, movementSM);
        walkingState = new WalkingState(this, movementSM);
        jumpState = new JumpState(this, movementSM, rb, animator);
        fallingState = new AirState(this, movementSM, animator);

        movementSM.Initialize(standingState);
    }

    private void Update()
    {
        movementSM.CurrentState.LogicUpdate();
    }
    
    private void FixedUpdate()
    {
        if (CanMove)
        {
            movementSM.CurrentState.PhysicsUpdate();
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }

        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive && CanMove)
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
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded && CanMove && IsAlive)
        {
            IsJumping = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(Ammo.AmmoAmount > 0)
            {
                animator.SetTrigger("RangedAttack");
            }
        }
    }

    public void OnHit(Vector2 knockback)
    {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void Shoot()
    {
        projectileSpawner.Spawn();
        Ammo.AmmoAmount--;
    }

    private void OnDisable()
    {
        Health.Pushed -= OnHit;
    }
}

