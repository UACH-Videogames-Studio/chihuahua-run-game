using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] GameObject obstaclesLine;
    public float generateTime = 1.0f;
    public float speedIncrementDown = 0.1f;
    [SerializeField] private float destroyTime = 10f;
    public float pauseTime = 2.0f;

    public static List<MovementDown> allMovementScripts = new List<MovementDown>();
    private Coroutine generateCoroutine;

    void Start()
    {
        generateCoroutine = StartCoroutine(GenerateObstacles());
    }

    private IEnumerator GenerateObstacles()
    {
        while (true)
        {
            GameObject newObstacleLine = Instantiate(obstaclesLine, new Vector3(0, 7, 0), Quaternion.identity);

            MovementDown[] movementScripts = obstaclesLine.GetComponentsInChildren<MovementDown>();

            allMovementScripts.AddRange(movementScripts);

            foreach (MovementDown movement in allMovementScripts)
            {
                movement.speed += speedIncrementDown;
                if (movement.speed > 5)
                {
                    generateTime = 0.75f;
                }
            }

            Destroy(newObstacleLine, destroyTime);

            StartCoroutine(RemoveScriptsAfterDestroy(movementScripts, destroyTime));

            yield return new WaitForSeconds(generateTime);
            //Destroy(obstaclesLine, destroyTime);
        }
    }

    private IEnumerator RemoveScriptsAfterDestroy(MovementDown[] scripts, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (MovementDown script in scripts)
        {
            allMovementScripts.Remove(script);
        }
    }

    public void StopGenerating()
    {
        if (generateCoroutine != null)
        {
            StopCoroutine(generateCoroutine);
            generateCoroutine = null;
        }
    }

    public void RestartGenerating()
    {
        if (generateCoroutine == null)
        {
            generateCoroutine = StartCoroutine(GenerateObstacles());
        }
    }

    public IEnumerator ResetObstaclesSpeed()
    {
        // Important Work whit a copy List because work whit original List will cause a exception
        List<MovementDown> allMovementScriptsCopy = new List<MovementDown>(allMovementScripts);

        foreach (MovementDown movement in allMovementScriptsCopy)
        {
            movement.speed = 0.5f; //Decrease speed to "player movement" (really the world movement)
        }

        yield return new WaitForSeconds(2.0f);

        foreach (MovementDown movement in allMovementScriptsCopy)
        {
            movement.speed = 3f; //Restore speed to "player movement" (really the world movement)
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
