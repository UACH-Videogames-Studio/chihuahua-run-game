using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The Scriptable object of this obstacle")] private ObstacleScriptableObject obstacleScriptableObject;
    //private Animator auxAnimator;
    [Header("Variables that you dont have to change")][Space(10)]
    [SerializeField][Tooltip("If it isn't assing, assign it in 40")] private float growingVelocityRegulator = 40;
    private float growingVelocity;
    private void Start()
    {
        //auxAnimator = GetComponent<Animator>();
        //auxAnimator = obstacleScriptableObject.ObstacleAnimator;
        this.growingVelocity = obstacleScriptableObject.ImpactVelocity / growingVelocityRegulator;
    }
    private void Update()
    {
        transform.position += Vector3.down * obstacleScriptableObject.ImpactVelocity * Time.deltaTime;
        transform.localScale += Vector3.one * growingVelocity * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameUIScript.Instance.QuitMomentum(obstacleScriptableObject.TakeAwayMoment);
        }
    }
}
