using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
