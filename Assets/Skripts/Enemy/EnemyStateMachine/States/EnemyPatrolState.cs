
public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, DetectionZone attackDetection, Health health) 
        : base(enemy, stateMachine, attackDetection, health)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if(health.IsAlive)
        {
            if (attackDetection.IsDetected)
            {
                stateMachine.ChangeState(enemy.chaseEState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (enemy.CanMove && health.IsAlive)
        {
            enemy.Patrol();
        }
        else
        {
            enemy.StopMovement();
        }
    }
}
