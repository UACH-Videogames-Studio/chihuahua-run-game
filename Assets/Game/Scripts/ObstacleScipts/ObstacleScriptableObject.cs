using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ScriptableObjects/Obstacle")] //File name: The default name menuName: Window/name
public class ObstacleScriptableObject : ScriptableObject
{
    [SerializeField] private new string name;
    public string Name { get => name; private set => name = value; }
    [SerializeField][Tooltip("Values between 0 and 1, where 1 is the maximum")] private float takeAwayMoment;
    public float TakeAwayMoment { get => takeAwayMoment; private set => takeAwayMoment = value; }
    [SerializeField][Tooltip("Its time to impact the player")] private float impactVelocity;
    public float ImpactVelocity { get => impactVelocity; set => impactVelocity = value; }
    // [SerializeField] private Sprite obstacleSprite;
    // public Sprite ObstacleSprite { get => obstacleSprite; private set => obstacleSprite = value; }
    [SerializeField] private bool canBeJumped;
    public bool CanBeJumped { get => canBeJumped; private set => canBeJumped = value; }
    [SerializeField] private bool isStatic;
    public bool IsStatic { get => isStatic; private set => isStatic = value; }
    // [SerializeField] private Animator obstacleAnimator;
    // public Animator ObstacleAnimator { get => obstacleAnimator; private set => obstacleAnimator = value; }
    [SerializeField] private float growingVelocity;
    public float GrowingVelocity { get => growingVelocity; set => growingVelocity = value; }
    [SerializeField] private float maximumSize;
    public float MaximumSize { get => maximumSize; set => maximumSize = value; }
}