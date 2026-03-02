using UnityEngine;

public class Skill_Base : MonoBehaviour
{

    public Player player { get; private set; }

    public ScaleEffectData damageScaleData {get; private set;}

    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    [SerializeField] protected float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        lastTimeUsed = -cooldown; // So the skill is available at the start of the game
    }

    public virtual void TryUseSkill()
    {
        
    }

    public void SetSkillUpgrade (UpgradeData upgrade)
    {
        upgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;
        damageScaleData = upgrade.damageScale;
    }

    public bool CanUseSkill()
    {
        if(upgradeType == SkillUpgradeType.None)
        {
            return false;
        }

        if (OnCooldown())
        {
            Debug.Log("Skill on cooldown");
            return false;
        }

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => upgradeType == upgradeToCheck;

    protected bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;
}
