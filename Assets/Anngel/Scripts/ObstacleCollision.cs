using System.Collections;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private Collider2D obstacleCollider;
    private ObstaclesGenerator obstaclesGenerator;

    void Start()
    {
        obstacleCollider = GetComponent<Collider2D>();
        obstaclesGenerator = FindFirstObjectByType<ObstaclesGenerator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Todo
            // Add speed bar's substracion. Add crash effect sound. Restore line obstacle generator's speed
            // obstaclesGenerator.StartCoroutine(obstaclesGenerator.ResetObstaclesSpeed());
            // obstaclesGenerator.StopGenerating();
            StartCoroutine(CrashPlayer(collision.gameObject));
        }
    }

    private IEnumerator CrashPlayer(GameObject player)
    {
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        int blinkCount = 15;
        float blinkDuration = 0.2f;

        for (int i = 0; i < blinkCount; i++)
        {
            playerSprite.enabled = !playerSprite.enabled;
            yield return new WaitForSeconds(blinkDuration);
        }
        playerSprite.enabled = true;
        yield return new WaitForSeconds(obstaclesGenerator.pauseTime);
        obstaclesGenerator.RestartGenerating();
    }
}
