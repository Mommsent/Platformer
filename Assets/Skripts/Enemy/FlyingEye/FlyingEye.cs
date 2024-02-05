using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public DetectionZone biteDetectionZone;
    public Collider2D deathCollider;


    public float flightSpeed = 3f;
    public List<Transform> waypoints;
    private int waypointNum = 0;
    private Transform nextWaypoint;
    public float waypintReachedDistance = 0.1f;

    private Animator animator;
    private Rigidbody2D rb;
    private FlyingEyeHealth health;

    private bool _hasTarget = false;
    

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
        private set { animator.SetBool("CanMove", value); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<FlyingEyeHealth>();
        health.Died += OnDeath;
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
        CanMove = true;
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (health.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;

        UpdateDirection();

        if(distance <= waypintReachedDistance)
        {
            waypointNum++;
            if(waypointNum >= waypoints.Count) 
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if(rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z) ;
            }
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }

    private void OnDisable()
    {
        health.Died -= OnDeath;
    }
}
