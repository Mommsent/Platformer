using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(KnightHealth))]
public class Knight : Enemy
{
    private float speed = 3f;
    [SerializeField] private DetectionZone attackZone;

    Rigidbody2D rb;

    KnightHealth KnightHealth;

    public List<Transform> waypoints;
    private int waypointNum = 0;
    private Transform nextWaypoint;
    public float waypointReachedDistance = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        KnightHealth = GetComponent<KnightHealth>();
        KnightHealth.Pushed += OnHit;
        KnightHealth.Died += OnDeath;
    }

    private void Start()
    {
        CanMove = true;
        CanAttack = false;
        AttackRange = 4;

        nextWaypoint = waypoints[waypointNum];

        stateMachine = new EnemyStateMachine();

        patrolEState = new EnemyPatrolState(this, stateMachine, attackZone, KnightHealth);
        chaseEState = new EnemyChaseState(this, stateMachine, attackZone, KnightHealth);
        attackEState = new EnemyAttackState(this, stateMachine, attackZone, KnightHealth);

        
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

    public override void Patrol()
    {
        MoveToAim(transform.position, nextWaypoint.position);
        UpdateDirection(transform.position, nextWaypoint.position);

        if (CheckDistanceToAim(transform.position, nextWaypoint.position) <= waypointReachedDistance)
        {
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    public override void UpdateDirection(Vector2 objectPos, Vector2 aimPos)
    {
        Vector2 directionToWaypoint = (aimPos - objectPos).normalized;
        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            if (directionToWaypoint.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if (directionToWaypoint.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
    }


    public override void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        Vector2 directionToWaypoint = (aimPos - objectPos).normalized;
        rb.velocity = new Vector2(directionToWaypoint.x * speed, 0);
        UpdateDirection(objectPos, aimPos);
    }
    public override float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos)
    {
        float distance = Vector2.Distance(AimPos, objectPos);
        return distance;
    }

    public void OnHit(Vector2 knockback)
    {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public override void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    public void OnDeath()
    {
        StopMovement();
    }

    private void OnDisable()
    {
        KnightHealth.Pushed -= OnHit;
        KnightHealth.Died += OnDeath;
    }
}
