using UnityEngine;

public class Player_DashState : PlayerState
{
    private float originalGravityScale;
    private int dashDir;

    public Player_DashState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;
        stateTimer = player.dashDuration;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
    }

    public override void Update()
    {
        base.Update();
        CalcelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * dashDir, 0f);

        if (stateTimer <= 0)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState); 
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0f, 0f);
        rb.gravityScale = originalGravityScale;
    }

    private void CalcelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}
