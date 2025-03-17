using UnityEngine;

public class MovementDown : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        // Delete this object from allMovementScripts list
        ObstaclesGenerator.allMovementScripts.Remove(this);
    }
}
