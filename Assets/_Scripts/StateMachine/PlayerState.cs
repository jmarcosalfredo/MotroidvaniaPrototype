using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;
    protected Player_SkillManager skillsManager;


    public PlayerState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
        stats = player.stats;
        skillsManager = player.skillManager;
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            skillsManager.dash.SetSkillOnCooldown();
            stateMachine.ChangeState(player.dashState);
        }

        if (input.Player.UltimateSpell.WasPressedThisFrame() && skillsManager.domainExpansion.CanUseSkill())
        {
            if (skillsManager.domainExpansion.InstantDomain())
            {
                skillsManager.domainExpansion.CreateDomain();
            }
            else
            {
                stateMachine.ChangeState(player.domainExpansionState);
            }

            skillsManager.domainExpansion.SetSkillOnCooldown();
        }
    }

    public override void UpdateAnimationParameters()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool CanDash()
    {
        if(skillsManager.dash.CanUseSkill() == false)
        {
            return false;
        }

        if (player.wallDetected)
        {
            return false;
        }

        if (stateMachine.currentState == player.dashState || stateMachine.currentState == player.domainExpansionState)
        {
            return false;
        }

        return true;
    }
}
