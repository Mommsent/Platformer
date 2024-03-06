using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FlyingEyeHealth))]
public class FlyingEye : Enemy
{
    [SerializeField] private Collider2D deathCollider;
    [SerializeField] private DetectionZone biteDetectionZone;

    private FlyingEyeHealth flyingEyehealth;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flyingEyehealth = GetComponent<FlyingEyeHealth>();

        flyingEyehealth.Died += OnDeath;
    }

    private void Start()
    {
        CanMove = true;
        CanAttack = false;

        nextWaypoint = waypoints[waypointNum];

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, biteDetectionZone, flyingEyehealth);
        chaseEState = new EnemyChaseState(this, stateMachine, biteDetectionZone, flyingEyehealth);
        attackEState = new EnemyAttackState(this, stateMachine, biteDetectionZone, flyingEyehealth);
        
        stateMachine.Initialize(patrolEState);
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    public override void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        directionToWaypoint = (aimPos - objectPos).normalized;
        rb.velocity = directionToWaypoint * speed;
    }

    public override void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, -rb.velocity.y);
        deathCollider.enabled = true;
        StopMovement();
    }

    private void OnDisable()
    {
        flyingEyehealth.Died -= OnDeath;
    }
}
