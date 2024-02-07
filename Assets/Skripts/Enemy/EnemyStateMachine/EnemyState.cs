
public abstract class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected DetectionZone attackDetection;
    protected Health health;
    protected EnemyState(Enemy enemy, EnemyStateMachine stateMachine, DetectionZone attackDetection, Health health)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.attackDetection = attackDetection;
        this.health = health;
    }
    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
