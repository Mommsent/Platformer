using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Animator animator;
    
    public float AttackRange {  get; set; }

    private bool canAttack = false;
    public bool CanAttack
    {
        get { return canAttack; }
        set
        {
            canAttack = value;
            animator.SetBool("CanAttack", value);
        }
    }

    private bool isAttacking = false;
    public bool IsAttacking
    {
        get { return isAttacking; }
        set
        {
            isAttacking = value;
            animator.SetBool("IsAttacking", value);
        }
    }

    public EnemyStateMachine stateMachine;
    public EnemyPatrolState patrolEState;
    public EnemyChaseState chaseEState;
    public EnemyAttackState attackEState;

    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
        set { animator.SetBool("CanMove", value); }
    }
    public abstract void Patrol();
    public abstract void MoveToAim(Vector2 objectPos, Vector2 aimPos);
    public abstract float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos);

    public abstract void UpdateDirection(Vector2 objectPos, Vector2 aimPos);
    public abstract void StopMovement();

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
