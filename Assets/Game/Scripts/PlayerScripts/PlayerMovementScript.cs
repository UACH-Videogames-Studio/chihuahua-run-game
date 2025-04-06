using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    private bool isInvencible, isACourutineStarted, isActivateTheJumpCourutine;
    private float inputMovement, newX, airTimeCounter = 0f, invencibleTimeCounter = 0f;
    private PolygonCollider2D playerCollider;
    private Animator playerAnimator;
    [HideInInspector] public SpriteRenderer playerSpriteRenderer;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            
            SafeCleanup();
            Destroy(gameObject);
            return;
        }
    
        Instance = this;
        DontDestroyOnLoad(gameObject);
    
        
        inputActions = new PenguinInputActions();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<PolygonCollider2D>();
        playerAnimator = GetComponent<Animator>();
    }
    private void SafeCleanup()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
            inputActions.Dispose();
        }
    }
    private void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<PolygonCollider2D>();
        playerAnimator = GetComponent<Animator>();
        isInvencible = false;
        isACourutineStarted = false;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        inputActions.Base.Jump.started += Jump;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        if (Instance != this) return;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (inputActions!=null)
        {
           inputActions.Base.Jump.started -= Jump;
            inputActions.Disable(); 
        }
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inputActions.Enable();
    }
    private void FixedUpdate()
    {
        if (isInvencible)
        {
            invencibleTimeCounter += Time.fixedDeltaTime * GameManager.Instance.timeMultiplier;
            if (invencibleTimeCounter >= invulnerabilityTime)
            {
                Vulnerable();
            }
        }
        if (isJumping)
        {
            airTimeCounter += Time.fixedDeltaTime * GameManager.Instance.timeMultiplier;
            if (airTimeCounter >= timeCanJump)
            {
                Land();
            }
        }
        inputMovement = inputActions.Base.Movement.ReadValue<float>();
        newX = Mathf.Clamp(transform.position.x + (velocity * inputMovement * Time.fixedDeltaTime * GameManager.Instance.timeMultiplier), -limitXAndY, limitXAndY);
        //The Mathf.Clamp(x, a, b) function limits a value x, in limits a, b
        transform.position = new Vector2(newX, transform.position.y);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if(!isJumping)
        {
            if(!isActivateTheJumpCourutine)
            {
                StartCoroutine(JumpCourutine());
            }
            isJumping = true;
            airTimeCounter = 0f;
            playerAnimator.SetTrigger("Jump");
        }
    }
    private void Land()
    {
        playerAnimator.SetTrigger("Jump");
        isJumping = false;
    }
    private void Vulnerable()
    {
        playerCollider.enabled = true;
        isInvencible = false;
        GameManager.Instance.ApplyFastUp();
    }
    public void HasBeenHitten()
    {
        GameManager.Instance.ApplySlowDown();
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
            yield return new WaitForSecondsRealtime(blinkDuration);
        }
        playerSpriteRenderer.enabled = true;
        isACourutineStarted = false;
    }
    private IEnumerator JumpCourutine()
    {
        isActivateTheJumpCourutine = true;
        float startY = transform.position.y;
        float endY = startY + 1f;
        float halfDuration = timeCanJump * 0.5f;
        float timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            float currentY = Mathf.Lerp(startY, endY, t);
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, currentY, pos.z);
            timer += Time.deltaTime * GameManager.Instance.timeMultiplier;
            yield return null;
        }
        timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            float currentY = Mathf.Lerp(endY, startY, t);
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, currentY, pos.z);
            timer += Time.deltaTime * GameManager.Instance.timeMultiplier;
            yield return null;
        }
        Vector3 finalPos = transform.position;
        transform.position = new Vector3(finalPos.x, startY, finalPos.z);
        isActivateTheJumpCourutine = false;
    }
}