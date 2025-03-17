using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstaclesLine;
    public float generateTime = 1.0f;
    public float speedIncrementDown = 0.1f;
    [SerializeField] private float destroyTime = 10f;

    public static List<MovementDown> allMovementScripts = new List<MovementDown>();


    void Start()
    {
        StartCoroutine(GenerateObstacles());
    }

    private IEnumerator GenerateObstacles()
    {
        while (true)
        {
            Instantiate(obstaclesLine, new Vector3(0, 7, 0), Quaternion.identity);

            MovementDown[] movementScripts = obstaclesLine.GetComponentsInChildren<MovementDown>();

            allMovementScripts.AddRange(movementScripts);

            foreach (MovementDown movement in allMovementScripts)
            {
                movement.speed += speedIncrementDown;
            }

            yield return new WaitForSeconds(generateTime);
            //Destroy(obstaclesLine, destroyTime);
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
