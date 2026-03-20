using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item List", fileName = "List of Items - ")]
public class ItemListDataSO : ScriptableObject
{
    public ItemDataSO[] itemList;
}
