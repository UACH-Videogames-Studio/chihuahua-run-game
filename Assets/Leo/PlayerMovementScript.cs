using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [HideInInspector] public PenguinInputActions inputActions;
    [SerializeField] private float limitXAndY;
    [SerializeField] private float velocity;
    private Rigidbody2D playerRB;
    public void Awake()
    {
        inputActions = new PenguinInputActions();
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        float inputMovement = inputActions.Base.Movement.ReadValue<float>();

        float newX = Mathf.Clamp(playerRB.position.x + (velocity * inputMovement * Time.deltaTime), -limitXAndY, limitXAndY);

        playerRB.MovePosition(new Vector2(newX, playerRB.position.y));
    }
}