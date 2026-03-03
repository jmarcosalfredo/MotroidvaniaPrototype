using UnityEngine;

public class Skill_SwordThrow : Skill_Base
{
    private SkillObject_Sword currentSword;

    [Header("Regular Sword Upgrade")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float throwPower = 5f;

    [Header("Trajectory Prediction")]
    [SerializeField] private GameObject predictionDotPrefab;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float spaceBetweenDots = 0.05f;
    private float swordGravity = 3.5f;
    private Transform[] dots;
    private Vector2 confirmedDirection;

    protected override void Awake()
    {
        base.Awake();
        swordGravity = swordPrefab.GetComponent<Rigidbody2D>().gravityScale;
        dots = GenerateDots();
    }

    public override bool CanUseSkill()
    {
        if(currentSword != null)
        {
            currentSword.GetSwordBackToPlayer();
            return false;
        }

        return base.CanUseSkill();
    }

    public void ThrowSword()
    {
        GameObject newSword = Instantiate(swordPrefab, transform.position, Quaternion.identity);

        currentSword = newSword.GetComponent<SkillObject_Sword>();
        currentSword.SetupSword(this, GetThrowPower());
    }

    private Vector2 GetThrowPower() => confirmedDirection * throwPower * 10f;

    public void PredictTrajectory(Vector2 direction)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].position = GetTrajectoryPoint(direction, i * spaceBetweenDots);
        }
    }

    private Vector2 GetTrajectoryPoint(Vector2 direction, float t)
    {
        float scaledThrowPower = throwPower * 10f;

        //This gives us initial velocity - the starting speed and direction of the throw.
        Vector2 initialVelocity = direction * scaledThrowPower;

        // Gravity pulls the sword down over time. The longer it's in the air, the more it drops. This simulates a natural arc.
        Vector2 gravityEffect = 0.5f * Physics2D.gravity * swordGravity * (t * t);

        //We calculate how far the sword will travel after time 't'.
        // by combining the initial throw direction with the gravity pull.
        Vector2 predictedPoint = (initialVelocity * t) + gravityEffect;

        Vector2 playerPosition = transform.root.position;

        return playerPosition + predictedPoint;
    }

    public void CorfirmedTrajectory(Vector2 direction) => confirmedDirection = direction;

    public void EnableDots(bool enable)
    {
        foreach (Transform dot in dots)
        {
            dot.gameObject.SetActive(enable);
        }
    }

    private Transform[] GenerateDots()
    {
        Transform[] newDots = new Transform[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            newDots[i] = Instantiate(predictionDotPrefab, transform.position, Quaternion.identity, transform).transform;
            newDots[i].gameObject.SetActive(false);
        }

        return newDots;
    }
}
