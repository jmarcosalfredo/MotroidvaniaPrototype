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

    public GameData()
    {
        inventory = new SerializableDictionary<string, int>();
        storageItems = new SerializableDictionary<string, int>();
        storageMaterials = new SerializableDictionary<string, int>();

        equippedItems = new SerializableDictionary<string, ItemType>();
    }

}
