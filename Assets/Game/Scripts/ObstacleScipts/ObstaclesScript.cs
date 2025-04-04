using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
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
    private static float sharedCurrentVelocity; //static variable permit that current velocity is stay between news instances, and that not reset velocity at original value
    private float initialImpactVelocity, currentSize, currentgrowingVelocity;
    private int side;
    // private ObstacleCollision obstacleCollision;
    //
    private void Awake()
    {
        initialImpactVelocity = obstacleScriptableObject.ImpactVelocity;
        //if is first instance initialize with initialImpactVelocity of ObstacleScriptableObject
        if (sharedCurrentVelocity == 0)
        {
            sharedCurrentVelocity = initialImpactVelocity;
        }
        currentVelocity = sharedCurrentVelocity;
    }
    private void Start()
    {
        // auxAnimator = GetComponent<Animator>();
        // auxAnimator = obstacleScriptableObject.ObstacleAnimator;
        //this.obstacleCollision = this.GetComponent<ObstacleCollision>();
        // this.growingVelocity = sharedCurrentVelocity / growingVelocityRegulator;
        SlowDown.allMovementScripts.Add(this);
        currentSize = 0;
        growingVelocityRegulator = 1;
    }
    private void OnEnable()
    {
        if (!this.obstacleScriptableObject.IsStatic)
        {
            this.side = Random.Range(-1, 2);
        }
        else
        {
            this.side = 0;
        }
    }
    private void Update()
    {
        sharedCurrentVelocity += Time.deltaTime * incrementSpeed;
        sharedCurrentVelocity = Mathf.Min(sharedCurrentVelocity, limitVelocity);
        currentVelocity = sharedCurrentVelocity;

        if (isSlowed)
        {
            currentVelocity = Mathf.Lerp(currentVelocity, obstacleScriptableObject.ImpactVelocity, speedRecoveryRate * Time.deltaTime);
            if (Mathf.Abs(currentVelocity - obstacleScriptableObject.ImpactVelocity) < 0.1f)
            {
                currentVelocity = obstacleScriptableObject.ImpactVelocity;
                growingVelocityRegulator = 1;
                isSlowed = false;
            }
        }
        // transform.position += Vector3.down * currentVelocity * Time.deltaTime;
        transform.position += new Vector3(currentVelocity * Time.deltaTime * side, -1 * currentVelocity * Time.deltaTime, 0);
        // transform.localScale += Vector3.one * this.obstacleScriptableObject.GrowingVelocity * Time.deltaTime;
        if (this.currentSize <= this.obstacleScriptableObject.MaximumSize)
        {
            this.transform.localScale += Vector3.one * this.obstacleScriptableObject.GrowingVelocity * growingVelocityRegulator  * Time.deltaTime;
            this.currentSize = transform.localScale.x;
        }
    }
    public void ApplySlowDown()
    {
        sharedCurrentVelocity = obstacleScriptableObject.ImpactVelocity * slowDownFactor;
        currentVelocity = sharedCurrentVelocity;
        growingVelocityRegulator = sharedCurrentVelocity;
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
            if(obstacleScriptableObject.CanBeJumped && PlayerMovementScript.Instance.isJumping)
            {
                AvoidTheObstacle();
            }
            else
            {
                PlayerMovementScript.Instance.HasBeenHitten();
                GameUIScript.Instance.QuitMomentum(obstacleScriptableObject.TakeAwayMoment);
                SlowDown.SlowAllObstacles();
            }
        }
    }
    private void OnDestroy()
    {
        SlowDown.allMovementScripts.Remove(this);
    }
    private void AvoidTheObstacle()
    {
        // this.obstacleCollision.StopactiveCrashCourutine(); //Stops the courutine
        // this.obstacleCollision.enabled = false;
    }
}