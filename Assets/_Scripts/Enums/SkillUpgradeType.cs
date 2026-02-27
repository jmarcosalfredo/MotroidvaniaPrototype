using UnityEngine;

public enum SkillUpgradeType
{
    None,
    // ------ Dash Tree ------
    Dash, // Dash to avoid damage
    Dash_CloneOnStart, //Create a clone when dash starts
    Dash_CloneOnStartAndArrival, //Create a clone when dash starts and arrives
    Dash_ShardOnStart, //Create a shard when dash starts
    Dash_ShardOnStartAndArrival, // Create a shard when dash starts and ends.

    // ------ Shard Tree ------
    Shard, // The shard explodes when touched by an enemy or when time runs out.
    Shard_MoveToEnemy, // Shard will move towards nearest enemy.
    Shard_TripleCast, // Shard ability can have up to N charges. Can cast all in a roll.
    Shard_TeleportAndHeal, // When you swap places with shard, your HP% is the same as it was when you created the shard.
}
