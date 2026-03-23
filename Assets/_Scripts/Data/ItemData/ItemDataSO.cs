using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Material Item", fileName = "Material Data - ")]
public class ItemDataSO : ScriptableObject
{
    [Header("Merchant Details")]
    [Range(0, 10000)]
    public int itemPrice = 100;
    public int minStackSizeAtShop = 1;
    public int maxStackSizeAtShop = 1;

    [Header("Drop Details")]
    [Range(0f, 1000f)]
    public int itemRarity = 100;
    [Range(0f, 100f)]
    public float dropChance;
    [Range(0f, 100f)]
    public float maxDropChance = 65f;

    [Header("Crafting Details")]
    public Inventory_Item[] craftRecipe; // Array of required items for crafting

    [Header("Item Details")]
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int maxStackSize = 1;

    [Header("Item Effect")]
    public ItemEffectDataSO itemEffect;

    private void OnValidate()
    {
        dropChance = GetDropChance();
    }

    public float GetDropChance()
    {
        float maxRarity = 1000f;
        float chance = (maxRarity - itemRarity + 1) / maxRarity * 100f;

        return Mathf.Min(chance, maxDropChance);
    }
}
