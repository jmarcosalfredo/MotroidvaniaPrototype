using UnityEngine;

public interface IDamageble
{

    public bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer);
}
