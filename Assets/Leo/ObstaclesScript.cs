using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    //
    [Header("Variables to assign")]
    [Space(10)]
    [SerializeField][Tooltip("The Scriptable object of this obstacle")] private ObstacleScriptableObject obstacleScriptableObject;
    [SerializeField] private float slowDownFactor = 0.5f;
    [SerializeField] private float speedRecoveryRate = 0.5f;
    [SerializeField] private float incrementSpeed = 0.1f;
    [SerializeField] private float limitVelocity = 20f;
    [SerializeField] private float currentVelocity, growingVelocity;
    //private Animator auxAnimator;

    [Header("Variables that you dont have to change")]
    [Space(10)]
    [SerializeField][Tooltip("If it isn't assing, assign it in 40")] private float growingVelocityRegulator = 40;

    private bool isSlowed = false;
    private float initialImpactVelocity;


    private void Awake()
    {
        initialImpactVelocity = obstacleScriptableObject.ImpactVelocity;
    }

    private void Start()
    {
        // auxAnimator = GetComponent<Animator>();
        // auxAnimator = obstacleScriptableObject.ObstacleAnimator;
        this.growingVelocity = obstacleScriptableObject.ImpactVelocity / growingVelocityRegulator;
        currentVelocity = obstacleScriptableObject.ImpactVelocity;
        SlowDown.allMovementScripts.Add(this);
    }
    private void Update()
    {
        currentVelocity += Time.deltaTime * incrementSpeed;
        currentVelocity = Mathf.Min(currentVelocity, limitVelocity);

        if (isSlowed)
        {
            currentVelocity = Mathf.Lerp(currentVelocity, obstacleScriptableObject.ImpactVelocity, speedRecoveryRate * Time.deltaTime);

            if (Mathf.Abs(currentVelocity - obstacleScriptableObject.ImpactVelocity) < 0.1f)
            {
                currentVelocity = obstacleScriptableObject.ImpactVelocity;
                isSlowed = false;
            }
        }
        transform.position += Vector3.down * currentVelocity * Time.deltaTime;
        transform.localScale += Vector3.one * growingVelocity * Time.deltaTime;
    }
    public void ApplySlowDown()
    {
        currentVelocity = obstacleScriptableObject.ImpactVelocity * slowDownFactor;
        isSlowed = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameUIScript.Instance.QuitMomentum(obstacleScriptableObject.TakeAwayMoment);
            SlowDown.SlowAllObstacles();
        }
    }
    private void OnDestroy()
    {
        SlowDown.allMovementScripts.Remove(this);
    }

    private void OnDisable()
    {
        obstacleScriptableObject.ImpactVelocity = initialImpactVelocity;
    }
}