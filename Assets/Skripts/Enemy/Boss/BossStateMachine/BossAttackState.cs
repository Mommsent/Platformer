using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    public BossAttackState(Boss boss, BossStateMachine stateMachine, DetectionZone attackDetection, Health health) : base(boss, stateMachine, attackDetection, health)
    {
    }

    public override void Enter()
    {
        Debug.Log("Im attack");
        boss.CanAttack = true;
        boss.CanMove = false;
    }

    public override void LogicUpdate()
    {
        if (!boss.IsAttacking && health.IsAlive)
        {
            boss.UpdateLookDirection(boss.transform.position, attackDetection.collisionPos);
            boss.UpdateSpriteFacingDirection(boss.transform.position, attackDetection.collisionPos);
        }

        bool isTooFar = boss.CheckDistanceToAim(boss.transform.position, attackDetection.collisionPos) > boss.AttackRange;
        if (isTooFar && !boss.IsAttacking)
        {
            stateMachine.ChangeState(boss.chaseEState);
        }
    }

    public override void Exit()
    {
        boss.CanAttack = false;
        boss.CanMove = true;
    }
}
