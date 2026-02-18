using UnityEngine;

public class Player_DeadState : PlayerState
{
    public Player_DeadState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        input.Disable();
        rb.simulated = false;
    }
}
