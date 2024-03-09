
public class BossStateMachine
{
    public BossState CurrentState { get; private set; }

    public void Initialize(BossState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(BossState newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
