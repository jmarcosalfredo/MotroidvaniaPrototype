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
    Shard_MultiCast, // Shard ability can have up to N charges. Can cast all in a roll.
    Shard_Teleport, // When you use the skill again, you swap places with the shard.
    Shard_TeleportHpRewind, // When you swap places with shard, your HP% is the same as it was when you created the shard.

    // ------ Sword Throw ------
    SwordThrow, // Throw a sword that damages enemies in its path.
    SwordThrow_Spin, // Sword will spin at one point and deal damage to enemies. Like a chainsaw.
    SwordThrow_Pierce, // Sword will pierce through N enemies and deal damage to all in its path.
    SwordThrow_Bounce, // Sword will bounce between enemies.

    // ------ Time Echo ------
    TimeEcho, // Create a clone of the player. It can distract enemies.
    TimeEcho_SinlgleAttack, // The clone will perform one attack.
    TimeEcho_MultiAttack, // The clone will perform multiple attacks.
    TimeEcho_ChanceToDuplicate, // The clone has a chance to create another clone when it attacks.

    TimeEcho_HealWisp, // When clone dies, it creates a heal wisp that heals the player.

    TimeEcho_CleanseWhisp, // Wisp will now remove negative effects from player.
    TimeEcho_CooldownWisp, // Wisp will reduce cooldown of all skills by N secounds.
}
