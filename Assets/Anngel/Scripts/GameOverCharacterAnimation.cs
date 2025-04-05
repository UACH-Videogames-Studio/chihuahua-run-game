using UnityEngine;

[CreateAssetMenu(fileName = "GameOverCharacterAnimation", menuName = "ScriptableObjects/GameOverCharacterAnimation")]
public class GameOverCharacterAnimation : ScriptableObject
{
    [Header("Animation Settings")]
    public float duration = 0.75f;
    [Range(0.1f, 10f)][SerializeField] private float animationSpeed = 1.0f;

    [Header("Enemy Data")]
    [SerializeField] private Texture enemySprite; //A raw image is used as a container for each enemy's sprite.
}