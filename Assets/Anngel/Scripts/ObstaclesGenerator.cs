using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public static ObstaclesGenerator Instance { get; private set; }
    [SerializeField] GameObject obstaclesLine;
    public float generateTime = 1.0f;
    public float speedIncrementDown = 0.1f;
    [SerializeField] private float destroyTime = 10f;
    public float pauseTime = 2.0f;
    public static bool isSlowedDown = false; //verify if is slowed speed (obstacles)
    [SerializeField] private float delayRegenerateSpeed = 1.5f;

    public static List<MovementDown> allMovementScripts = new List<MovementDown>();
    private Coroutine generateCoroutine;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } 
        else 
        {
            Instance = this;
        }
    }
    private void Start()
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

            if (isSlowedDown)
            {
                foreach (MovementDown movement in movementScripts)
                {
                    movement.speed = 0.5f;
                }
            }

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
        isSlowedDown = true;

        // Important Work whit a copy List because work whit original List will cause a exception
        List<MovementDown> allMovementScriptsCopy = new List<MovementDown>(allMovementScripts);

        foreach (MovementDown movement in allMovementScriptsCopy)
        {
            if (movement != null)
            {
                movement.speed = 0.5f;
                movement.targetSpeed = 0.5f;
            }
            //Decrease speed to "player movement" (really the world movement)
        }

        yield return new WaitForSeconds(delayRegenerateSpeed);

        isSlowedDown = false;
        foreach (MovementDown movement in allMovementScriptsCopy)
        {
            if (movement != null)
            {
                movement.targetSpeed = 2f;
            }
            //Restore speed to "player movement" (really the world movement)
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
