using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(enemy.groundDetected == false || enemy.wallDetected)
        {
            enemy.Flip();
        }
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if(enemy.groundDetected == false || enemy.wallDetected)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
