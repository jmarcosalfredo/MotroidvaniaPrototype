using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInBattle;
    public Enemy_BattleState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        if (player == null) // same as player ??= enemy.GetPlayerReference();
        {
            player = enemy.GetPlayerReference();
        }

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }

    }

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerDetection() == true)
        {
            UpdateBattleTimer();
        }

        if (BattleTimeOver())
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (WithinAttackRange() && enemy.PlayerDetection() == true)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

    private void UpdateBattleTimer()
    {
        lastTimeWasInBattle = Time.time;
    }

    private bool BattleTimeOver()
    {
        return Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;
    }

    private bool WithinAttackRange()
    {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private bool ShouldRetreat()
    {
        return DistanceToPlayer() < enemy.minRetreatDistance;
    }

    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }
        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

}
