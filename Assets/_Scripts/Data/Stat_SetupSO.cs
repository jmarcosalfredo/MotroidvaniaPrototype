using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stat Setup", fileName = "Default Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Resource")]
    public float maxHealth = 100f;
    public float healthRegen = 0;

    [Header("Offense - Physical Damage")]
    public float attackSpeed = 1f;
    public float damage = 10f;
    public float critChance = 0f;
    public float critPower = 0f;
    public float armorReduction = 0f;

    [Header("Offense - Elemental Damage")]
    public float fireDamage = 0f;
    public float iceDamage = 0f;
    public float lightningDamage = 0f;

    [Header("Defense - Physical Resistance")]
    public float armor = 0f;
    public float evasion = 0f;

    [Header("Defense - Elemental Resistance")]
    public float fireResistance = 0f;
    public float iceResistance = 0f;
    public float lightningResistance = 0f;

    [Header("Major Stats")]
    public float strength = 0f;
    public float agility = 0f;
    public float intelligence = 0f;
    public float vitality = 0f;
}
