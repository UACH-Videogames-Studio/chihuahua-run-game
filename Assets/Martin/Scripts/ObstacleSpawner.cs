using System.Collections.Generic;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    private LaneManager laneManager;
    [SerializeField] private List<GameObject> obstaclesPrefablist = new List<GameObject>();
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistanceForward = 20f;
    private float spawnTimer;
    private int randomLaneIndex, randomPrefabIndex;
    private GameObject auxGameObject;
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
    private void SpawnObstacle()
    {
        if (laneManager == null || obstaclesPrefablist == null || obstaclesPrefablist.Count == 0)
            return;
        
        randomLaneIndex = Random.Range(0, laneManager.GetLaneCount());
        Transform choosenLaneTransform = laneManager.GetLaneAtIndex(randomLaneIndex);
        if (choosenLaneTransform == null)
            return;

        randomPrefabIndex = Random.Range(0, obstaclesPrefablist.Count); //Get a random gameobject of the list
        auxGameObject = obstaclesPrefablist[randomPrefabIndex]; //It assign it in a another variable

        Vector3 spawnPosition = choosenLaneTransform.position +Vector3.up * spawnDistanceForward;

        Instantiate(auxGameObject, spawnPosition, Quaternion.identity);
    }
}
