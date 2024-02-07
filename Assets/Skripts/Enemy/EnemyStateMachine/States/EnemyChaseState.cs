using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, DetectionZone attackDetection, Health health) 
        : base(enemy, stateMachine, attackDetection, health)
    {
    }

    public override void Enter()
    {
        Debug.Log("I see you");
    }

    public override void LogicUpdate()
    {
        if (attackDetection.IsDetected)
        {
            enemy.MoveToAim(enemy.transform.position, attackDetection.collisionPos);
            if (enemy.CheckDistanceToAim(enemy.transform.position, attackDetection.collisionPos) < 2)
            {
                enemy.StopMovement();
                stateMachine.ChangeState(enemy.attackEState);
            }
        }
        else
        {
            stateMachine.ChangeState(enemy.patrolEState);
        }
    }
}
