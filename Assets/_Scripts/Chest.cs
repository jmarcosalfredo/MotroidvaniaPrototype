using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Chest : MonoBehaviour , IDamageble
{
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 openKnockback;

    public bool TakeDamage(float damage, Transform damageDealer)
    {
        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = openKnockback;

        rb.angularVelocity = Random.Range(-200f, 200f);

        //Drop Items
        return true;
    }
}
