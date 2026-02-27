using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{
    [SerializeField] private GameObject explosionVfxPrefab;

    public void SetupShard(float detonationTimer)
    {
        Invoke(nameof(Explode), detonationTimer);
    }

    private void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        Instantiate(explosionVfxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() == null)
        {
            return;

        }

        Explode();
    }
}
