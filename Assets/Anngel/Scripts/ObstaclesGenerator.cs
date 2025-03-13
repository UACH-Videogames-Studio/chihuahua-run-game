using System.Collections;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstaclesLine;

    void Start()
    {
        StartCoroutine(GenerateObstacles());
    }

    private IEnumerator GenerateObstacles()
    {
        while (true)
        {
            Instantiate(obstaclesLine, new Vector3(0, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
