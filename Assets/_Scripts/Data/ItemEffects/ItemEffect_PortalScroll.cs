using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Portal Scroll", fileName = "Item Effect Data - PortalScroll")]
public class ItemEffect_PortalScroll : ItemEffectDataSO
{
    public override void ExecuteEffect()
    {
        if (SceneManager.GetActiveScene().name == "Level_0")
        {
            Debug.LogWarning("Player is already in the town scene. Portal Scroll has no effect.");
            return;
        }

        Player player = Player.instance;
        Vector3 portalPosition = player.transform.position + new Vector3(player.facingDir * 1.5f, 0);

        Object_Portal.instance.ActivatePortal(portalPosition, player.facingDir);
    }
}
