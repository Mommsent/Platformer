using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, DetectionZone attackDetection, Health health) 
        : base(enemy, stateMachine, attackDetection, health)
    {
    }

    public override void Enter()
    {
        Debug.Log("Im attack");
        enemy.CanAttack = true;
        enemy.CanMove = false;
    }

    public override void LogicUpdate()
    {
        if(!enemy.IsAttacking && health.IsAlive)
        {
            enemy.UpdateLookDirection(enemy.transform.position, attackDetection.collisionPos);
            enemy.UpdateSpriteFacingDirection(enemy.transform.position, attackDetection.collisionPos);
        }

        bool isTooFar = enemy.CheckDistanceToAim(enemy.transform.position, attackDetection.collisionPos) > enemy.AttackRange;
        if (isTooFar && !enemy.IsAttacking)
        {
            stateMachine.ChangeState(enemy.chaseEState);
        }
        if (!attackDetection.IsDetected && !enemy.IsAttacking)
        {
            stateMachine.ChangeState(enemy.patrolEState);
        }
    }

    public override void Exit()
    {
        enemy.CanAttack = false;
        enemy.CanMove = true;
    }
}
