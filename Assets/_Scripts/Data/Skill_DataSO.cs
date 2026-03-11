using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill Data", fileName = "Skill Data - ")]
public class Skill_DataSO : ScriptableObject
{
    [Header("Skill Description")]
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;

    [Header("Unlocked & Upgrade")]
    public int cost;
    public bool unlockedByDefault;
    public SkillType skillType;
    public UpgradeData upgradeData;
}

[System.Serializable]
public class UpgradeData
{
    public SkillUpgradeType upgradeType;
    public float cooldown;
    public ScaleEffectData damageScale;
}
