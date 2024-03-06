using UnityEngine;

public class JumpState : State
{
    private float speed = 4f;
    private float jumpImpulse = 23f;
    private bool isJumping;
    Animator animator;
    Rigidbody2D rb;

    public JumpState(Player player, StateMachine stateMachine, Rigidbody2D rb, Animator animator) : base(player, stateMachine)
    {
        this.animator = animator;
        this.rb = rb;
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(player.IsOnWall)
        {
            player.CurrentMoveSpeed = 0;
        }
        else
        {
            player.CurrentMoveSpeed = speed;
        }

        if (player.IsGrounded)
        {
            player.IsJumping = false;
            isJumping = false;
        }

        if (!isJumping && player.IsGrounded)
        {
            if (player.IsMoving)
            {
                stateMachine.ChangeState(player.walkingState);
            }
            stateMachine.ChangeState(player.standingState);
        }
    }
    private void Jump()
    {
        isJumping = true;
        animator.SetTrigger("Jump");
        rb.velocity = new Vector2(rb.velocity.x * speed, jumpImpulse);
        player.CurrentMoveSpeed = speed;
    }
}
