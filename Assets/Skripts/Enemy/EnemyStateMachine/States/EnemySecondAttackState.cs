using UnityEngine;

public class EnemySecondAttackState : EnemyAttackState
{
    Animator animator;
    public EnemySecondAttackState(Enemy enemy, EnemyStateMachine stateMachine, DetectionZone attackDetection, Health health, Animator animator) 
        : base(enemy, stateMachine, attackDetection, health)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        animator.SetBool("CanUseSecondAttack", true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        animator.SetBool("CanUseSecondAttack", false);
    }
}
