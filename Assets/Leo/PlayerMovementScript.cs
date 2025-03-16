using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [HideInInspector] public PenguinInputActions inputActions;
    [SerializeField] private float limitXAndY;
    [SerializeField] private float velocity;
    public void Awake()
    {
        inputActions = new PenguinInputActions();
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

        float newX = Mathf.Clamp(transform.position.x + (velocity * inputMovement * Time.deltaTime), -limitXAndY, limitXAndY);
        //The Mathf.Clamp(x, a, b) function limits a value x, in limits a, b

        transform.position = new Vector2(newX, transform.position.y);
    }
}