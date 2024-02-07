using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(KnightHealth))]
public class Knight : Enemy
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    public float waypintReachedDistance = 1f;

    Rigidbody2D rb;

    KnightHealth KnightHealth;

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

    private float walkStopeRate = 0.005f;
  
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
        KnightHealth = GetComponent<KnightHealth>();
        KnightHealth.Pushed += OnHit;
    }

    private void Start()
    {
        CanMove = true;
        CanAttack = false;

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, attackZone, KnightHealth);
        chaseEState = new EnemyChaseState(this, stateMachine, attackZone, KnightHealth);
        attackEState = new EnemyAttackState(this, stateMachine, attackZone, KnightHealth);

        
        stateMachine.Initialize(patrolEState);
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if(AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
        if (AttackCooldown < 0)
            AttackCooldown = 0.5f;
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();

        if (!_touchingDirection.IsGrounded && _touchingDirection.IsOnWall)
        {
            FlipDirection();
        }

        if(!KnightHealth.LockVelocity)
        {
            
        }
    }

    public override void Patrol()
    {
        float xVelocity = Mathf.Clamp(
                    rb.velocity.x + (walkAcceleration * walkableDirectionVector.x * Time.deltaTime), -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public override void StopMovement()
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopeRate), rb.velocity.y);
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

    public override void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        Vector2 directionToWaypoint = (aimPos - objectPos).normalized;
        rb.velocity = directionToWaypoint * maxSpeed;
    }

    public void OnHit(Vector2 knockback)
    {
        KnightHealth.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnNoGroundDetected()
    {
        if(_touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }

    public override float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos)
    {
        float distance = Vector2.Distance(AimPos, objectPos);
        return distance;
    }

    private void OnDisable()
    {
        KnightHealth.Pushed -= OnHit;
    }
}
