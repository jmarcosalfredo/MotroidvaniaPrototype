using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Refund Skill Points", fileName = "Item Effect Data - Refund Skill Points")]
public class ItemEffect_RefundSkillPoints : ItemEffectDataSO
{
    public override void ExecuteEffect()
    {
        UI ui = FindFirstObjectByType<UI>();
        ui.skillTree.RefundAllSkills();
    }
}
