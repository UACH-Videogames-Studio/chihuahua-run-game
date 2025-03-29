using System.Collections;
using UnityEngine;
public class ObstacleCollision : MonoBehaviour
{
    [Header("Variables to assign")]
    [Space(10)]
    [SerializeField][Tooltip("The default value is 15")] private int blinkCount = 15;
    [SerializeField][Tooltip("The default value is 0.2")] private float blinkDuration = 0.2f;
    private Collider2D obstacleCollider;
    private ObstaclesGenerator obstaclesGenerator;
    private Coroutine activeCrashCourutine; // We create another courutine just to save the actual courutine
    void Start()
    {
        obstacleCollider = GetComponent<Collider2D>();
        obstaclesGenerator = FindFirstObjectByType<ObstaclesGenerator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.enabled)
        {
            //Todo
            // Add speed bar's substracion. Add crash effect sound. Restore line obstacle generator's speed
            // obstaclesGenerator.StartCoroutine(obstaclesGenerator.ResetObstaclesSpeed());
            // obstaclesGenerator.StopGenerating();
            // StartCoroutine(CrashPlayer(collision.gameObject));
            activeCrashCourutine = StartCoroutine(CrashPlayer(collision.gameObject));
        }
    }
    private IEnumerator CrashPlayer(GameObject player)
    {
        if(activeCrashCourutine != null) //With these if, we check if is a courutine active, and now, we can't have 2 or more actives courutines
        {
            StopCoroutine(activeCrashCourutine);
        }

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        activeCrashCourutine = StartCoroutine(BlinkRoutine());

        yield return activeCrashCourutine;

        playerSprite.enabled = true;
        yield return new WaitForSeconds(obstaclesGenerator.pauseTime);
        obstaclesGenerator.RestartGenerating();
        activeCrashCourutine = null;
        PlayerMovementScript.Instance.playerSpriteRenderer.enabled = true;
        
        IEnumerator BlinkRoutine()
        {
            for (int i = 0; i < blinkCount; i++)
            {
                if (i == blinkCount - 1) playerSprite.enabled = true; //This line fixs partially the issue
                if (!this.enabled)
                {
                    playerSprite.enabled = true;
                    yield break;
                }
                playerSprite.enabled = !playerSprite.enabled;
                yield return new WaitForSeconds(blinkDuration);
            }
            playerSprite.enabled = true;
        }
    }
    public void StopactiveCrashCourutine()
    {
        if (activeCrashCourutine != null)
        {
            StopCoroutine(activeCrashCourutine);
            activeCrashCourutine = null;
        }

        // Makes sure that te player is alwas visible
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            if (playerSprite != null) playerSprite.enabled = true;
        }
    }
}
