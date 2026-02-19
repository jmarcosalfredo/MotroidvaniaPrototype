using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    //Physical dmg
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;
    
    //Elemental dmg
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
