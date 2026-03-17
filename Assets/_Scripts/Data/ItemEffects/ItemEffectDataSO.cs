using UnityEngine;

public class ItemEffectDataSO : ScriptableObject // Base class for item effects
{
    [TextArea]
    public string effectDescription;

    public virtual void ExecuteEffect()
    {
        // Implement the effect logic here
    }
}
