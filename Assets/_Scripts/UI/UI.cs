using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip { get; private set; }
    public UI_ItemToolTip itemToolTip { get; private set; }
    public UI_StatToolTip statToolTip { get; private set; }

    public UI_SkillTree skillTreeUI { get; private set; }
    public UI_Inventory inventoryUI { get; private set; }
    public UI_Storage storageUI { get; private set; }

    [SerializeField] private Animator playerIconAnimator;

    private bool skillTreeEnabled;
    private bool inventoryEnabled;

    private void SetPlayerIconAnimatorState()
    {
        if (playerIconAnimator != null)
            playerIconAnimator.enabled = inventoryEnabled;
    }

    private void Awake()
    {
        itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
        statToolTip = GetComponentInChildren<UI_StatToolTip>();

        skillTreeUI = GetComponentInChildren<UI_SkillTree>(true);
        inventoryUI = GetComponentInChildren<UI_Inventory>(true);
        storageUI = GetComponentInChildren<UI_Storage>(true);

        if (playerIconAnimator == null)
            Debug.LogError("UI: playerIconAnimator nao foi atribuido no Inspector.", this);

        skillTreeEnabled = skillTreeUI.gameObject.activeSelf;
        inventoryEnabled = inventoryUI.gameObject.activeSelf;

        SetPlayerIconAnimatorState();
    }

    public void SwitchOffAllToolTips()
    {
        itemToolTip.ShowToolTip(false, null);
        skillToolTip.ShowToolTip(false, null);
        statToolTip.ShowToolTip(false, null);
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTreeUI.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }

    public void ToggleInventoryUI()
    {
        inventoryEnabled = !inventoryEnabled;
        SetPlayerIconAnimatorState();
        inventoryUI.gameObject.SetActive(inventoryEnabled);
        statToolTip.ShowToolTip(false, null);
        itemToolTip.ShowToolTip(false, null);
    }
}