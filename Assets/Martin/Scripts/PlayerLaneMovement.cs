using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaneMovement : MonoBehaviour
{
    [SerializeField] private LaneManager laneManager;

    [SerializeField] private int currentLaneIndex = 1;
    [SerializeField] private float playerHorizontalSpeed = 10f;

    [SerializeField] private InputActionReference moveAction;

    private Transform currentLanePosition;

    private void OnEnable()
    {
        moveAction.action.Enable();
        moveAction.action.performed += OnMovePerformed;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.Disable();
    }
    void Start()
    {
        if (laneManager != null)
        {
            currentLanePosition = laneManager.GetLaneAtIndex(currentLaneIndex);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLanePosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentLanePosition.position, playerHorizontalSpeed * Time.deltaTime);
        }

    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        float inputMovement = context.ReadValue<float>();

        if (inputMovement < 0f)
        {
            if (currentLaneIndex > 0)
            {
                currentLaneIndex--;
            }
        }
        else if (inputMovement > 0f)
        {
            if (currentLaneIndex < laneManager.GetLaneCount() - 1)
            {
                currentLaneIndex++;
            }
        }
       
        if (laneManager != null)
        {
            currentLanePosition = laneManager.GetLaneAtIndex(currentLaneIndex);
        }
    }
}
