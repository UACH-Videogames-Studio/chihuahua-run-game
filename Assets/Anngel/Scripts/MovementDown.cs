using UnityEngine;

public class MovementDown : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
