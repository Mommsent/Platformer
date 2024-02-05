
public class StandingState : GroundedState
{
    private float speed;
    public StandingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = 0;
        player.CurrentMoveSpeed = speed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.CanMove && player.IsMoving)
        {
            stateMachine.ChangeState(player.walkingState);
        }
        
        if (player.IsJumping)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
