using UnityEngine;

public class AirState : State
{
    private float speed = 4f;
    private Animator animator;
    public AirState(Player player, StateMachine stateMachine, Animator animator) : base(player, stateMachine)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("IsFalling", true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsOnWall)
        {
            player.CurrentMoveSpeed = 0;
        }
        else
        {
            player.CurrentMoveSpeed = speed;
        }

        if (player.IsGrounded)
        {
            animator.SetBool("IsFalling", false);
            if (player.IsMoving)
            {
                stateMachine.ChangeState(player.walkingState);
            }
            stateMachine.ChangeState(player.standingState);
        }
    }
}
