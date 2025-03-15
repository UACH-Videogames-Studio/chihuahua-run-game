using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ScriptableObjects/Obstacle")] //File name: The default name menuName: Window/name
public class ObstacleScriptableObject : ScriptableObject
{
    [SerializeField] new string name;
    public string Name {get => name; private set => name = value;}
    [SerializeField][Tooltip("Values between 0 and 1, where 1 is the maximum")] float takeAwayMoment;
    public float TakeAwayMoment {get => takeAwayMoment; private set => takeAwayMoment = value;}
    [SerializeField] Sprite obstacleSprite;
    public Sprite ObstacleSprite {get => obstacleSprite; private set => obstacleSprite = value;}
    [SerializeField] bool canBeJumped;
    public bool CanBeJumped {get => canBeJumped; private set => canBeJumped = value;}
    [SerializeField] Animator obstacleAnimator;
    public Animator ObstacleAnimator {get => obstacleAnimator; private set => obstacleAnimator = value;}
}