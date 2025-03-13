using System.Collections;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstaclesLine;
    public float generateTime = 1.0f;

    void Start()
    {
        StartCoroutine(GenerateObstacles());
    }

    private IEnumerator GenerateObstacles()
    {
        while (true)
        {
            Instantiate(obstaclesLine, new Vector3(0, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(generateTime);
            generateTime += 0.1f;
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
