using UnityEngine;

public class BossChaseState : BossState
{
    public BossChaseState(Boss boss, BossStateMachine stateMachine, DetectionZone attackDetection, Health health) 
        : base(boss, stateMachine, attackDetection, health)
    {
    }

    public override void Enter()
    {
        Debug.Log("I see you");
    }

    public override void LogicUpdate()
    {
        if (health.IsAlive && boss.CanMove)
        {
            boss.MoveToAim(boss.transform.position, attackDetection.collisionPos);
            boss.UpdateSpriteFacingDirection(boss.transform.position, attackDetection.collisionPos);
            if (boss.CheckDistanceToAim(boss.transform.position, attackDetection.collisionPos) < boss.AttackRange)
            {
                boss.StopMovement();
                stateMachine.ChangeState(boss.attackEState);
            }
        }
    }
}
