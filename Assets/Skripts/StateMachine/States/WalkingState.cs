


public class WalkingState : GroundedState
{
    private float walkSpeed = 5f;
    private float runningSpeed = 9f;
    public WalkingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.CurrentMoveSpeed = walkSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CurrentMoveSpeed = walkSpeed;
        if (player.IsJumping)
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (!player.IsGrounded)
        {
            stateMachine.ChangeState(player.fallingState);
        }
        if (player.IsRunning)
        {
            player.CurrentMoveSpeed = runningSpeed;
        }

        if (!player.CanMove || !player.IsMoving)
        {
            stateMachine.ChangeState(player.standingState);
        }

        
    }
}
