using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementScript : MonoBehaviour
{
    public static PlayerMovementScript Instance { get; private set; }
    [HideInInspector] public PenguinInputActions inputActions;
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The default value is 8")] private float limitXAndY;
    [SerializeField][Tooltip("The default velocity is 12")] private float velocity;
    [SerializeField][Tooltip("The time that the player can jump")] private float timeCanJump;
    [SerializeField][Tooltip("The time that the player is inmortal")] private float invulnerabilityTime;
    [SerializeField][Tooltip("The blinks duration time")] private float blinkDuration = 0.2f;
    [HideInInspector] public bool isJumping;
    private bool isInvencible, isACourutineStarted;
    private float inputMovement, newX, airTimeCounter = 0f, invencibleTimeCounter = 0f;
    private PolygonCollider2D playerCollider;
    [HideInInspector] public SpriteRenderer playerSpriteRenderer;
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
        inputActions = new PenguinInputActions();
    }
    private void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<PolygonCollider2D>();
        isInvencible = false;
        isACourutineStarted = false;
    }
    private void OnEnable()
    {
        inputActions.Base.Jump.started += Jump;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Base.Jump.started -= Jump;
        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        inputMovement = inputActions.Base.Movement.ReadValue<float>();

        newX = Mathf.Clamp(transform.position.x + (velocity * inputMovement * Time.deltaTime), -limitXAndY, limitXAndY);
        //The Mathf.Clamp(x, a, b) function limits a value x, in limits a, b

        transform.position = new Vector2(newX, transform.position.y);
    }
    private void Update()
    {
        if (isInvencible)
        {
            invencibleTimeCounter += Time.deltaTime;
            if (invencibleTimeCounter >= invulnerabilityTime)
            {
                Vulnerable();
            }
        }
        if (isJumping)
        {
            airTimeCounter += Time.deltaTime;
            if (airTimeCounter >= timeCanJump)
            {
                Land();
            }
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if(!isJumping)
        {
            isJumping = true;
            airTimeCounter = 0f;
        }
    }
    private void Land()
    {
        isJumping = false;
    }
    private void Vulnerable()
    {
        playerCollider.enabled = true;
        isInvencible = false;
    }
    public void HasBeenHitten()
    {
        invencibleTimeCounter = 0f;
        playerCollider.enabled = false;
        isInvencible = true;
        if (!isACourutineStarted)
        {
            StartCoroutine(BlinkCourutine());
        }
    }
    private IEnumerator BlinkCourutine()
    {
        isACourutineStarted = true;
        while (isInvencible)
        {
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            yield return new WaitForSeconds(blinkDuration);
        }

        playerSpriteRenderer.enabled = true;
        isACourutineStarted = false;
        ObstaclesGenerator.Instance.RestartGenerating();
    }
}