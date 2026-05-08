using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int gold;
    public SerializableDictionary<string, int> inventory; // Key: itemSaveId, Value: stackSize
    public SerializableDictionary<string, int> storageItems;
    public SerializableDictionary<string, int> storageMaterials;
    
    public SerializableDictionary<string, ItemType> equippedItems; // Key: itemSaveId, Value: slotType

    public int skillPoints;
    public SerializableDictionary<string, bool> skillTreeUI; // Key: skill name, Value: isUnlocked
    public SerializableDictionary<SkillType, SkillUpgradeType> skillUpgrades; // Key: SkillType, Value: SkillUpgradeType

    public SerializableDictionary<string, bool> unlockedCheckpoints; // Key: checkpoint ID, Value: isUnlocked
    public SerializableDictionary<string, Vector3> inScenePortals; // Key: scene name, Value: portal position

    public string portalDestinationSceneName;
    public bool returningFromTown;


    public GameData()
    {
        inventory = new SerializableDictionary<string, int>();
        storageItems = new SerializableDictionary<string, int>();
        storageMaterials = new SerializableDictionary<string, int>();

        equippedItems = new SerializableDictionary<string, ItemType>();

        skillTreeUI = new SerializableDictionary<string, bool>();
        skillUpgrades = new SerializableDictionary<SkillType, SkillUpgradeType>();

        unlockedCheckpoints = new SerializableDictionary<string, bool>();
        inScenePortals = new SerializableDictionary<string, Vector3>();
    }

}
