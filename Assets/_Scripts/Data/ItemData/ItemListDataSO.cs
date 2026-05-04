using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item List", fileName = "List of Items - ")]
public class ItemListDataSO : ScriptableObject
{
    public ItemDataSO[] itemList;

    public ItemDataSO GetItemData( string saveID)
    {
        return itemList.FirstOrDefault( item => item != null && item.saveId == saveID);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill a list with all ItemDataSO assets in the project")]
    public void CollectItemsData()
    {
        string [] guids = AssetDatabase.FindAssets("t:ItemDataSO");

        itemList = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<ItemDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
