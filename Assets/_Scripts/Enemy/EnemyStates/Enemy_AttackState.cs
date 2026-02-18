using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
