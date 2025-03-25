using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    //
    private LaneManager laneManager;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistanceForward = 20f;
    private float spawnTimer;
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
        if (laneManager == null || obstaclePrefab == null)
            return;
        
        int randomLaneIndex = Random.Range(0, laneManager.GetLaneCount());
        Transform choosenLaneTransform = laneManager.GetLaneAtIndex(randomLaneIndex);

        if (choosenLaneTransform == null)
            return;

        Vector3 spawnPosition = choosenLaneTransform.position +Vector3.up * spawnDistanceForward;

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
