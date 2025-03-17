using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private Collider2D obstacleCollider;

    void Start()
    {
        obstacleCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Todo
            // Add speed bar's substracion. Add crash effect sound. Restore line obstacle generator's speed
            StartCoroutine(CrashPlayer(collision.gameObject));
        }
    }

    private IEnumerator CrashPlayer(GameObject player)
    {
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        int blinkCount = 15;
        float blinkDuration = 0.2f;

        foreach (MovementDown movement in ObstaclesGenerator.allMovementScripts)
        {
            //Restore speed to "player movement" (really the world movement)
            movement.speed = 3.0f;
        }

        for (int i = 0; i < blinkCount; i++)
        {
            playerSprite.enabled = !playerSprite.enabled;
            yield return new WaitForSeconds(blinkDuration);
        }
        playerSprite.enabled = true;
    }
}
