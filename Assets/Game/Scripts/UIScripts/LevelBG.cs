using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelBG : MonoBehaviour
{
    [Header("Variables to assign")][Space(10)]
    [Tooltip("The sprites that the background needs")][SerializeField] private List<Sprite> sprites= new List<Sprite>();
    [Tooltip("The time that the sprites are waiting")][SerializeField] private float timeBetweenFrames;
    private SpriteRenderer bGRenderer;
    private int currentSpriteIndex = 0;
    private void Start()
    {
        bGRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = 0;
        StartCoroutine(MapCourutine());
    }
    private IEnumerator MapCourutine()
    {
        while (GameManager.Instance.isPlay)
        {
            bGRenderer.sprite = sprites[currentSpriteIndex];
            yield return new WaitForSeconds(timeBetweenFrames);
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
        }
    }
}
