using System.Collections;
using UnityEngine;

public class FlyAcrossUIAnimationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pigeonPrefab;
    [SerializeField] private RectTransform pigeonContainer;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private float spawnPosX = -415f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPigeon(); ;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void SpawnPigeon()
    {
        GameObject newPigeon = Instantiate(pigeonPrefab, pigeonContainer);
        RectTransform rt = newPigeon.GetComponent<RectTransform>();
        float randomY = Random.Range(minY, maxY);
        rt.anchoredPosition = new Vector2(spawnPosX, randomY);

    }



}
