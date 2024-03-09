using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Knight : Enemy
{
    [SerializeField] private DetectionZone attackZone;

    Health KnightHealth;

    private void Awake()
    {
        KnightHealth = GetComponent<Health>();
        KnightHealth.Pushed += OnHit;
        KnightHealth.Died += OnDeath;
    }

    private void Start()
    {
        CanMove = true;
        CanAttack = false;

        nextWaypoint = waypoints[waypointNum];

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, attackZone, KnightHealth);
        chaseEState = new EnemyChaseState(this, stateMachine, attackZone, KnightHealth);
        attackEState = new EnemyAttackState(this, stateMachine, attackZone, KnightHealth);

        
        stateMachine.Initialize(patrolEState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    public void OnHit(Vector2 knockback)
    {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void OnDisable()
    {
        KnightHealth.Pushed -= OnHit;
        KnightHealth.Died += OnDeath;
    }
}
