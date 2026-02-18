using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter-Attack Details")]
    [SerializeField] private float counterRecovery = 0.1f;
    public bool CounterAttackPerformed()
    {
        bool counterAttackSuccessful = false;

        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if(counterable == null)
            {
                continue;
            }

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                counterAttackSuccessful = true;
            }
        }

        return counterAttackSuccessful;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;
}
