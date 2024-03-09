using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private SceneController sceneController;
    Health health;

    private int hpNeededForStageTwo;
    private int hpNeededForStageThree;

    [SerializeField] private float speed = 3f;

    private Vector2 aim;
    [SerializeField] private float attackRange = 2f;

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public bool CanAttack
    {
        get { return animator.GetBool("CanAttck"); }
        set { animator.SetBool("CanAttack", value); }
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

    public bool CanDoubleAttack
    {
        get { return animator.GetBool("CanDoubleAttack"); }
        set { animator.SetBool("CanDoubleAttack", value); }
    }

    public bool CanTripleAttack
    {
        get { return animator.GetBool("CanTripleAttack"); }
        set { animator.SetBool("CanTripleAttack", value); }
    }

    private BossStateMachine stateMachine;
    public BossChaseState chaseEState;
    public BossAttackState attackEState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.Died += OnDeath;
    }

    private void Start()
    {
        hpNeededForStageTwo = health.MaxHealth / 100 * 70;
        hpNeededForStageThree = health.MaxHealth / 100 * 50;

        stateMachine = new BossStateMachine();

        chaseEState = new BossChaseState(this, stateMachine, attackZone, health);
        attackEState = new BossAttackState(this, stateMachine, attackZone, health);

        stateMachine.Initialize(chaseEState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if(health.CurrentHealth < hpNeededForStageTwo)
        {
            CanDoubleAttack = true;
        }
        if (health.CurrentHealth < hpNeededForStageThree)
        {
            CanTripleAttack = true;
        }
    }

    public void MoveToAim(Vector2 objectPos, Vector2 aimPos)
    {
        aim = (aimPos - objectPos).normalized;
        rb.velocity = new Vector2(aim.x * speed, 0);
    }

    public void UpdateLookDirection(Vector2 objectPos, Vector2 aimPos)
    {
        aim = (aimPos - objectPos).normalized;
    }

    public float CheckDistanceToAim(Vector2 objectPos, Vector2 AimPos)
    {
        float distance = Vector2.Distance(AimPos, objectPos);
        return distance;
    }

    public void UpdateSpriteFacingDirection(Vector2 objectPos, Vector2 aimPos)
    {

        Vector3 localScale = transform.localScale;
        if (aim.x < 0)
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
    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    public void OnDeath()
    {
        StopMovement();
        sceneController.LoadNextLevevl();
    }

    private void OnDisable()
    {
        health.Died += OnDeath;
    }
}
