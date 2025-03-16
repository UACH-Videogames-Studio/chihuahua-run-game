using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    [SerializeField][Tooltip("The Scriptable object of this obstacle")] private ObstacleScriptableObject obstacleScriptableObject;
    //private Animator auxAnimator;
    private float growingVelocity;
    private void Start()
    {
        //auxAnimator = GetComponent<Animator>();
        //auxAnimator = obstacleScriptableObject.ObstacleAnimator;
        this.growingVelocity = obstacleScriptableObject.ImpactVelocity / 40;
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
