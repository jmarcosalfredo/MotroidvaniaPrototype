using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConectorHandler connectorHandler;

    [Header("Unlock Details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;


    [Header("Skill Details")]
    [SerializeField] private Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    [SerializeField] private Color lockedColor = new Color32(0x9F, 0x97, 0x97, 0xFF);
    private Color lastColor;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectorHandler = GetComponent<UI_TreeConectorHandler>();

        UpdateIconColor(lockedColor);
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(lockedColor);

        skillTree.AddSkillPoints(skillData.cost);
        connectorHandler.UnlockConnectionImage(false);
    }

    public Skill_DataSO GetSkillData()
    {
        return skillData;
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoints(skillData.cost);
        connectorHandler.UnlockConnectionImage(true);

        skillTree.skillManager.GetSkillByType(skillData.skillType).SetSkillUpgrade(skillData.upgradeType);
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
        {
            return false;
        }

        if (skillTree.EnoughSkillPoints(skillData.cost) == false)
        {
            return false;
        }

        foreach (var node in neededNodes)
        {
            if (node.isUnlocked == false)
            {
                return false;
            }
        }

        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked)
            {
                return false;
            }
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
        {
            node.isLocked = true;
        }
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
        {
            return;
        }

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
        {
            Unlock();
        }
        else if (isLocked)
        {
            ui.skillToolTip.LockedSkillEffect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (isUnlocked || isLocked)
        {
            return;
        }
            ToggleNodeHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (isUnlocked|| isLocked)
        {
            return;
        }
            ToggleNodeHighlight(false);
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * 0.9f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;
        UpdateIconColor(colorToApply);
    }

    private void OnDisable()
    {
        if (isLocked)
        {
            UpdateIconColor(lockedColor);
        }
         if (isUnlocked)
        {
            UpdateIconColor(Color.white);
        }
    }

    private void OnValidate()
    {
        if (skillData == null)
        {
            return;
        }

        skillName = skillData.skillName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name = "UI_TreeNode - " + skillName;
    }
}
