
public class BossState
{
    protected Boss boss;
    protected BossStateMachine stateMachine;
    protected DetectionZone attackDetection;
    protected Health health;

    protected BossState(Boss boss, BossStateMachine stateMachine, DetectionZone attackDetection, Health health)
    {
        this.boss = boss;
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
