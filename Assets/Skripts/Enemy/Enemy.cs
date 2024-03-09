using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rb;

    [SerializeField] internal float speed = 3f;

    [SerializeField] internal List<Transform> waypoints;
    internal int waypointNum = 0;
    internal Transform nextWaypoint;
    [SerializeField] private float waypointReachedDistance = 0.5f;
    internal Vector2 directionToWaypoint;

    [SerializeField] private float attackRange = 2f;
    public float AttackRange 
    {  
        get{ return attackRange; }
        set{ attackRange = value; }
    }

    public bool CanAttack
    {
        get { return animator.GetBool("CanAttck"); }
        set { animator.SetBool("CanAttack", value);}
    }

    public bool IsAttacking
    {
        get { return animator.GetBool("IsAttacking"); }
        set { animator.SetBool("IsAttacking", value); }
    }
    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
        set { animator.SetBool("CanMove", value); }
    }

    public EnemyStateMachine stateMachine;
    public EnemyPatrolState patrolEState;
    public EnemyChaseState chaseEState;
    public EnemyAttackState attackEState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Patrol()
    {
        if (CheckDistanceToAim(transform.position, nextWaypoint.position) <= waypointReachedDistance)
        {
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
        MoveToAim(transform.position, nextWaypoint.position);
        UpdateSpriteFacingDirection(transform.position, nextWaypoint.position);
    }
    public virtual void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        directionToWaypoint = (aimPos - objectPos).normalized;
        rb.velocity = new Vector2(directionToWaypoint.x * speed, 0);
    }
    public virtual void UpdateLookDirection(Vector2 objectPos, Vector2 aimPos)
    {
        directionToWaypoint = (aimPos - objectPos).normalized;
    }
    public virtual float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos)
    {
        float distance = Vector2.Distance(AimPos, objectPos);
        return distance;
    }

    public virtual void UpdateSpriteFacingDirection(Vector2 objectPos, Vector2 aimPos)
    {
        
        Vector3 localScale = transform.localScale;
        if (directionToWaypoint.x < 0)
        {
            if (localScale.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if (localScale.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
    }
    public virtual void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    public virtual void OnDeath()
    {
        StopMovement();
    }
}
