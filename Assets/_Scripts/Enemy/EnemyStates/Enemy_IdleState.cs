using UnityEngine;

public class Enemy_IdleState : Enemy_GroundedState
{
    public Enemy_IdleState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
