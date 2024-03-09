using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Goblin : Enemy
{
    [SerializeField] private DetectionZone bombDetectionZone;
    [SerializeField] private DetectionZone secondAttackZone;
    private EnemySecondAttackState secondAttackEState;

    Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.Pushed += OnHit;
        health.Died += OnDeath;
    }

    private void Start()
    {
        CanMove = true;
        CanAttack = false;

        nextWaypoint = waypoints[waypointNum];

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, bombDetectionZone, health);
        chaseEState = new EnemyChaseState(this, stateMachine, bombDetectionZone, health);
        attackEState = new EnemyAttackState(this, stateMachine, bombDetectionZone, health);
        secondAttackEState = new EnemySecondAttackState(this, stateMachine, secondAttackZone, health, animator);


        stateMachine.Initialize(patrolEState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
        if(secondAttackZone.IsDetected)
        {
            stateMachine.ChangeState(secondAttackEState);
        }
    }

    public void OnHit(Vector2 knockback)
    {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void OnDisable()
    {
        health.Pushed -= OnHit;
        health.Died += OnDeath;
    }
}
