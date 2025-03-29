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
    [HideInInspector] public bool isJumping;
    private float inputMovement, newX, airTimeCounter = 0f;
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
}