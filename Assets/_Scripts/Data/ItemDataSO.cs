using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Material Item", fileName = "Material Data - ")]
public class ItemDataSO : ScriptableObject
{
    public string ItemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int maxStackSize = 1;
}
