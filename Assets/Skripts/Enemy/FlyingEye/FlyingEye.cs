using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FlyingEyeHealth))]
public class FlyingEye : Enemy
{
    public Collider2D deathCollider;

    private Rigidbody2D rb;

    public DetectionZone biteDetectionZone;
    public float flightSpeed = 3f;

    private FlyingEyeHealth flyingEyehealth;

    public List<Transform> waypoints;
    private int waypointNum = 0;
    private Transform nextWaypoint;
    public float waypointReachedDistance = 2f;
    
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

    public override void Patrol()
    {
        MoveToAim(transform.position, nextWaypoint.position);
        UpdateDirection();

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

    public override float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos)
    {
        float distance = Vector2.Distance(AimPos, objectPos);
        return distance;
    }


    public override void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    public override void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        Vector2 directionToWaypoint = (aimPos - objectPos).normalized;
        rb.velocity = directionToWaypoint * flightSpeed;
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
    }

    private void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, -rb.velocity.y);
        deathCollider.enabled = true;
    }

    private void OnDisable()
    {
        flyingEyehealth.Died -= OnDeath;
    }
}
