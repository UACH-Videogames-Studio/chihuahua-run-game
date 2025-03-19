using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovementDown : MonoBehaviour
{
    public float speed = 5.0f;
    public float limitVelocity = 15.0f;

    void Update()
    {
        speed = Mathf.Min(speed, limitVelocity);
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        // Delete this object from allMovementScripts list
        ObstaclesGenerator.allMovementScripts.Remove(this);
    }
}
