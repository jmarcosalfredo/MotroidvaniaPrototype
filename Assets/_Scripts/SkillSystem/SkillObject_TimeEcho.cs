using UnityEngine;

public class SkillObject_TimeEcho : SkillObject_Base
{
    [SerializeField] private float wispMoveSpeed = 15f;
    [SerializeField] private GameObject onDeathVfxPrefab;
    [SerializeField] private LayerMask whatIsGround;
    private bool shouldMoveToPlayer;

    private Transform playerTransform;
    private Skill_TimeEcho timeEchoManager;
    private TrailRenderer wispTrail;
    private Entity_Health playerHealth;
    private SkillObject_Health echoHealth;
    private Player_SkillManager skillManager;
    private Entity_StatusHandler statusHandler;

    public int maxAttacks { get; private set; }

    public void SetupEcho(Skill_TimeEcho timeEchoManager)
    {
        this.timeEchoManager = timeEchoManager;
        playerStats = timeEchoManager.player.stats;
        damageScaleData = timeEchoManager.damageScaleData;
        maxAttacks = timeEchoManager.GetMaxAttacks();
        playerTransform = timeEchoManager.transform.root;
        playerHealth = timeEchoManager.player.health;
        skillManager = timeEchoManager.skillManager;
        statusHandler = timeEchoManager.player.statusHandler;

        Invoke(nameof(HandleDeath), timeEchoManager.GetEchoDuration());
        FlipToTarget();

        echoHealth = GetComponent<SkillObject_Health>();
        wispTrail = GetComponentInChildren<TrailRenderer>();
        wispTrail.gameObject.SetActive(false);

        anim.SetBool("canAttack", maxAttacks > 0);
    }

    private void Update()
    {
        if (shouldMoveToPlayer)
        {
            HandleWispMovement();
        }
        else
        {
            anim.SetFloat("yVelocity", rb.linearVelocity.y);
            StopHorizontalMovement();
        }
    }

    private void HandleWispMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, wispMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
        {
            HandleWispEffect();
            Destroy(gameObject);
        }
    }

    private void HandleWispEffect()
    {
        float healAmount = echoHealth.lastDamageTaken * timeEchoManager.GetPercentOfDamageHealed();
        playerHealth.IncreaseHealth(healAmount);

        float cooldownToReduce = timeEchoManager.GetCooldownReducedInSecounds();
        skillManager.ReduceAllSkillCooldownBy(cooldownToReduce);

        if (timeEchoManager.CanRemoveNegativeEffects())
        {
            statusHandler.RemoveAllNegativeEffects();
        }
    }

    private void FlipToTarget()
    {
        Transform target = ClosestTarget();

        if (target != null && target.position.x < transform.position.x)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    public void PerformAttack()
    {
        DamageEnemiesInRadius(targetCheck, 1);

        if (targetGotHit == false)
        {
            return;
        }

        bool canDuplicate = Random.value < timeEchoManager.GetDuplicateChance();
        float xOffset = transform.position.x < lastTarget.position.x ? 1 : -1;

        if (canDuplicate)
        {
            timeEchoManager.CreateTimeEcho(lastTarget.position + new Vector3(xOffset, 0, 0));
        }
    }

    public void HandleDeath()
    {
        Instantiate(onDeathVfxPrefab, transform.position, Quaternion.identity);

        if (timeEchoManager.ShouldBeWisp())
        {
            TurnIntoWisp();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void TurnIntoWisp()
    {
        shouldMoveToPlayer = true;
        anim.gameObject.SetActive(false);
        wispTrail.gameObject.SetActive(true);
        rb.simulated = false;
    }

    private void StopHorizontalMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, whatIsGround);

        if (hit.collider != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
