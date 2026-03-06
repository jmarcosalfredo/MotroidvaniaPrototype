using UnityEngine;

public class SkillObject_TimeEcho : SkillObject_Base
{
    [SerializeField] private GameObject onDeathVfxPrefab;
    [SerializeField] private LayerMask whatIsGround;
    private Skill_TimeEcho timeEchoManager;

    public void SetupEcho(Skill_TimeEcho timeEchoManager)
    {
        this.timeEchoManager = timeEchoManager;

        Invoke(nameof(HandleDeath), timeEchoManager.GetEchoDuration());
    }

    private void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        StopHorizontalMovement();
    }

    public void HandleDeath()
    {
        Instantiate(onDeathVfxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void StopHorizontalMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, whatIsGround);

        if(hit.collider != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
