using UnityEngine;
public class TopeScript : MonoBehaviour
{
    private Transform topeTransform;
    private void Start()
    {
        topeTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        topeTransform.position = new Vector3(0, topeTransform.position.y, 0);
    }
}
