using UnityEngine;

public class ItemEffectDataSO : ScriptableObject // Base class for item effects
{
    [TextArea]
    public string effectDescription;
    protected Player player;

    public virtual bool CanBeUsed(Player player)
    {
        return true;
    }

    public virtual void ExecuteEffect()
    {
        // Override the effect logic here
    }

    public virtual void Subscribe (Player player)
    {
        this.player = player;
    }

    public virtual void Unsubscribe()
    {

    }
}
