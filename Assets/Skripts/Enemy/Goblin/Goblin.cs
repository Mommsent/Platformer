using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Goblin : Enemy
{
    [SerializeField] private DetectionZone _bombDetectionZone;
    [SerializeField] private DetectionZone _secondAttackZone;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    private EnemySecondAttackState _secondAttackEState;

    Health health;

    private void OnEnable()
    {
        health.Pushed += OnHit;
        health.Died += OnDeath;
    }

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, _bombDetectionZone, health);
        chaseEState = new EnemyChaseState(this, stateMachine, _bombDetectionZone, health);
        attackEState = new EnemyAttackState(this, stateMachine, _bombDetectionZone, health);
        _secondAttackEState = new EnemySecondAttackState(this, stateMachine, _secondAttackZone, health, animator);


        stateMachine.Initialize(patrolEState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
        if(_secondAttackZone.IsDetected)
        {
            stateMachine.ChangeState(_secondAttackEState);
        }
    }

    public void OnHit(Vector2 knockback)
    {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void Throw()
    {
        _projectileSpawner.Spawn();
    }

    private void OnDisable()
    {
        health.Pushed -= OnHit;
        health.Died -= OnDeath;
    }
}
