using System.Collections.Generic;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }
    private LaneManager laneManager;
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("Is the list where goes all of the obstacles prefabs of the level")] private List<GameObject> obstaclesPrefablist = new List<GameObject>();
    [SerializeField][Tooltip("The default value is 2f")] private float spawnInterval;
    [SerializeField][Tooltip("The default value is 3f")] private float spawnDistanceForward;
    private float spawnTimer;
    private int randomLaneIndex, randomPrefabIndex;
    private GameObject auxGameObject;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        } 
        else 
        {
            Instance = this;
        }
    }
    private void SpawnObstacle()
    {
        if (laneManager == null || obstaclesPrefablist == null || obstaclesPrefablist.Count == 0) return;

        randomLaneIndex = Random.Range(0, laneManager.GetLaneCount());
        Transform choosenLaneTransform = laneManager.GetLaneAtIndex(randomLaneIndex);

        if (choosenLaneTransform == null) return;

        randomPrefabIndex = Random.Range(0, obstaclesPrefablist.Count); //Get a random gameobject of the list
        auxGameObject = obstaclesPrefablist[randomPrefabIndex]; //It assign it in a another variable

        Vector3 spawnPosition = choosenLaneTransform.position + Vector3.up * spawnDistanceForward;
        Instantiate(auxGameObject, spawnPosition, Quaternion.identity);
    }
    public void ReduceTimeToSpawnObstacles(float time)
    {
        spawnTimer -= time;
    }
    private void Start()
    {
        laneManager = GetComponentInChildren<LaneManager>();
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            SpawnObstacle();
        } 
    }
}