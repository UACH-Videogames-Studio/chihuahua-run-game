using UnityEngine;
public class ObstaclesMovementScript : MonoBehaviour
{
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The Scriptable object of this obstacle")] private ObstacleScriptableObject obstacleScriptableObject;
    private float currentSize;
    private int side;
    private void Start()
    {
        currentSize = 0;
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
        transform.position += new Vector3( Time.deltaTime * side, -1 * Time.deltaTime * this.obstacleScriptableObject.ImpactVelocity, 0);
        if (this.currentSize <= this.obstacleScriptableObject.MaximumSize)
        {
            this.transform.localScale += Vector3.one * this.obstacleScriptableObject.GrowingVelocity * Time.deltaTime;
            this.currentSize = transform.localScale.x;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!this.obstacleScriptableObject.CanBeJumped || !PlayerMovementScript.Instance.isJumping)
            {
                PlayerMovementScript.Instance.HasBeenHitten();
                GameUIScript.Instance.QuitMomentum(obstacleScriptableObject.TakeAwayMoment);
            }
        }
    }
}
