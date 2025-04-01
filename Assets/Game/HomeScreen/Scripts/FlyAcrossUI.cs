using UnityEngine;

public class FlyAcrossUI : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        
        if (rt.anchoredPosition.x > 450)
        {
            Destroy(gameObject);
        }
    }
}
