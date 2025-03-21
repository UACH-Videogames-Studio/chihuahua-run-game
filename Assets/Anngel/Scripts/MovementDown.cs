using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovementDown : MonoBehaviour
{
    public float speed = 5.0f;
    public float limitVelocity = 15.0f;
    public float targetSpeed = 3.0f;
    public float acceleration = 1.0f;

    void Update()
    {
        if (ObstaclesGenerator.isSlowedDown)
        {
            targetSpeed = 0.5f;
        }
        else
        {
            targetSpeed = 3.0f;
        }

        speed = Mathf.Lerp(speed, targetSpeed, acceleration * Time.deltaTime);
        speed = Mathf.Min(speed, limitVelocity);

        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        // Delete this object from allMovementScripts list
        ObstaclesGenerator.allMovementScripts.Remove(this);
    }
}
