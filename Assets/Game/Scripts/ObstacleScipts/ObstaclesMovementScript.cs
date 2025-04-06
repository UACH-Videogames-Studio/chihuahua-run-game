using System.Collections;
using UnityEngine;
public class ObstaclesMovementScript : MonoBehaviour
{
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The Scriptable object of this obstacle")] private ObstacleScriptableObject obstacleScriptableObject;
    private AudioSource obstacleAudioSource;
    private float currentSize;
    private int side;
    private void Start()
    {
        currentSize = 0;
        this.obstacleAudioSource = this.gameObject.GetComponent<AudioSource>();
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
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!this.obstacleScriptableObject.CanBeJumped || !PlayerMovementScript.Instance.isJumping)
            {
                // if(this.obstacleScriptableObject.ObstacleAudioHit != null)
                // {
                //     this.obstacleAudioSource.PlayOneShot(this.obstacleScriptableObject.ObstacleAudioHit);
                // }
                // PlayerMovementScript.Instance.HasBeenHitten();
                // GameUIScript.Instance.QuitMomentum(obstacleScriptableObject.TakeAwayMoment);
                // Destroy(this.gameObject);
                PlayerMovementScript.Instance.HasBeenHitten();
                GameUIScript.Instance.QuitMomentum(this.obstacleScriptableObject.TakeAwayMoment);
                StartCoroutine(DestroyAfterSound());
            }
            else if (PlayerMovementScript.Instance.isJumping && this.obstacleScriptableObject.CanBeJumped)
            {
                StartCoroutine(DestroyCourutine());
            }
        }
    }
    private IEnumerator DestroyAfterSound()
{
    this.obstacleAudioSource.PlayOneShot(this.obstacleScriptableObject.ObstacleAudioHit);
    yield return new WaitForSeconds(this.obstacleScriptableObject.ObstacleAudioHit.length);
    Destroy(this.gameObject);
}
    private IEnumerator DestroyCourutine()
    {
        yield return new WaitForSeconds(0.7f * GameManager.Instance.timeMultiplier);
        Destroy(this.gameObject);
    }
}
