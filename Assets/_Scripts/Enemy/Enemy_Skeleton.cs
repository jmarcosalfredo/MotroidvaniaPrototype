using UnityEngine;

public class Enemy_Skeleton : Enemy, ICounterable
{
    public bool CanBeCountered { get => canBeStunned; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy_IdleState(stateMachine, "idle", this);
        moveState = new Enemy_MoveState(stateMachine, "move", this);
        attackState = new Enemy_AttackState(stateMachine, "attack", this);
        battleState = new Enemy_BattleState(stateMachine, "battle", this);
        deadState = new Enemy_DeadState(stateMachine, "idle", this);
        stunnedState = new Enemy_StunnedState(stateMachine, "stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }


    [ContextMenu("Stun Enemy")]
    public void HandleCounter()
    {
        if(CanBeCountered == false)
        {
            return;
        }

        stateMachine.ChangeState(stunnedState);
    }
}

