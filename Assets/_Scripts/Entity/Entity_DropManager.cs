using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity_DropManager : MonoBehaviour
{
    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private ItemListDataSO dropListData;

    [Header("Drop Settings")]
    [SerializeField] private int maxRarityAmount = 1200;
    [SerializeField] private int maxItemsToDrop = 3;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            DropItems();
        }
    }

    public virtual void DropItems()
    {
        if(dropListData == null)
        {
            return;
        }

        List<ItemDataSO> itemsToDrop = RollDrops();
        int amountToDrop = Mathf.Min(itemsToDrop.Count, maxItemsToDrop);

        for (int i = 0; i < amountToDrop; i++)
        {
            CreateItemToDrop(itemsToDrop[i]);
        }
    }

    protected void CreateItemToDrop(ItemDataSO itemToDrop)
    {
        GameObject newItem = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        newItem.GetComponent<Object_ItemPickup>().SetupItem(itemToDrop);
    }

    public List<ItemDataSO> RollDrops()
    {
        List<ItemDataSO> possibleDrops = new List<ItemDataSO>();
        List<ItemDataSO> finalDrops = new List<ItemDataSO>();
        float maxRarityAmount = this.maxRarityAmount;

        // Roll each item based on rarity and max drop chance.
        foreach (var item in dropListData.itemList)
        {
            float dropChance = item.GetDropChance();

            if(Random.Range(0f, 100f) <= dropChance)
            {
                possibleDrops.Add(item);
            }
        }

        // Sort by rarity (highest to lowest).
        possibleDrops = possibleDrops.OrderByDescending(item => item.itemRarity).ToList();

        // Add items to final drop list until rarity limit on entity is reached.
        foreach (var item in possibleDrops)
        {
            if (maxRarityAmount > item.itemRarity)
            {
                finalDrops.Add(item);
                maxRarityAmount -= item.itemRarity;
            }
        }

        return finalDrops;
    }
}
