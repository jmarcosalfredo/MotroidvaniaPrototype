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
}
