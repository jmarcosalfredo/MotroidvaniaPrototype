using System;
using UnityEngine;

public class SkillObject_DomainExpansion : SkillObject_Base
{
    private Skill_DomainExpansion domainManager;

    private float expandSpeed = 5;
    private float duration;
    private float slowDownPercent = 0.9f;

    private Vector3 targetScale;
    private bool isShrinking;


    public void SetupDomain(Skill_DomainExpansion domainManager)
    {
        this.domainManager = domainManager;

        duration = domainManager.GetDomainDuration();
        float maxSize = domainManager.GetMaxDomainSize();
        slowDownPercent = domainManager.GetSlowDownPercentage();
        expandSpeed = domainManager.GetExpandSpeed();

        targetScale = Vector3.one * maxSize;
        Invoke(nameof(ShrinkDomain), duration);
    }

    private void Update()
    {
        HandleScaling();
    }

    private void HandleScaling()
    {
        float sizeDifference = Math.Abs(transform.localScale.x - targetScale.x);
        bool shouldChangeScale = sizeDifference > 0.1f;

        if (shouldChangeScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * expandSpeed);
        }

        if (isShrinking && sizeDifference < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void ShrinkDomain()
    {
        targetScale = Vector3.zero;
        isShrinking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SlowDownEntity(duration, slowDownPercent, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
        {
            return;
        }

        enemy.StopSlowDown();
    }
}
