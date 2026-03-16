using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatToolTip : UI_ToolTip
{
    private Player_Stats playerStats;
    private TextMeshProUGUI statToolTipText;

    protected override void Awake()
    {
        base.Awake();
        playerStats = FindFirstObjectByType<Player_Stats>();
        statToolTipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowToolTip(bool show, RectTransform targetRect, StatType statType)
    {
        base.ShowToolTip(show, targetRect);
        statToolTipText.text = GetStatTextByType(statType);
    }

    public string GetStatTextByType(StatType type)
    {
        switch (type)
        {
            //Major Stats
            case StatType.Strength:
                return "Increases physical damage by 1 per point." +
                    "\n Increases critical power by 0.5% per point.";
            case StatType.Agility:
                return "Increases critical chance by 0.3% per point." +
                       "\n Increases evasion by 0.5% per point.";
            case StatType.Inteligence:
                return "Increases elemental resistances by 0.5% per point." +
                       "\n Adds 1 elemental damage per point as a bonus. \nIf all elements have 0 damage the bonus will not be applied.";
            case StatType.Vitality:
                return "Increases max health by 5 per point." +
                       "\n Increases armor by 1 per point.";

            //Physical Damage
            case StatType.Damage:
                return "Determines the physical damage of your attacks.";
            case StatType.CritChance:
                return "Determines the chance to deal a critical hit.";
            case StatType.CritPower:
                return "Increase the damage dealt by critical hits.";
            case StatType.ArmorReduction:
                return "Percent of enemies armor ignored by your attacks.";
            case StatType.AttackSpeed:
                return "Determines how quickly you can attack.";

            //Defensive Stats
            case StatType.MaxHealth:
                return "Determines the maximum health of your character.";
            case StatType.HealthRegen:
                return "Determines how much health you regenerate per second.";
            case StatType.Armor:
                return "Reduces incoming physical damage." +
                        "\n Limited at 85% damage reduction." +
                        "\n Current mitigation is:" + playerStats.GetArmorMitigation(0) + "%";
            case StatType.Evasion:
                return "Chance to completely avoid an incoming attack." + 
                        "\n Limited at 85% evasion.";

            //Elemental Damage
            case StatType.IceDamage:
                return "Adds ice damage to your attacks." +
                    "\n Ice damage slow enemies.";
            case StatType.FireDamage:
                return "Adds fire damage to your attacks." +
                    "\n Fire damage causes enemies to take damage over time.";
            case StatType.LightningDamage:
                return "Adds lightning damage to your attacks." +
                    "\n If stacked, Lightning damage deal a lighting stance of damage in a small area.";
            case StatType.ElementalDamage:
                return "Elemental damage combine all elements." +
                    "\n The highest element will determine the elemental effect applied to the enemy." +
                    "\n The other elements contribute with 50% of their damage as a bonus.";

            //Elemental Resistances
            case StatType.IceResistance:
                return "Reduces incoming ice damage." +
                    "\n Ice resistance reduces the slow effect of ice damage.";
            case StatType.FireResistance:
                return "Reduces incoming fire damage." +
                    "\n Fire resistance reduces the damage over time effect of fire damage.";
            case StatType.LightningResistance:
                return "Reduces incoming lightning damage." +
                    "\n Lightning resistance reduces the damage of the lightning stance.";
            default:
                return "No description available.";
        }
    }
}
