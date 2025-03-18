using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLineMovement : MonoBehaviour
{
    [SerializeField] private Transform lineContainer;
    [SerializeField] private Transform[] linesPosition;
    [SerializeField] private Transform actualPosition;
    [SerializeField] private int lineIndex = 1;
    [SerializeField] private float playerHorizontalSpeed = 10f;
    [SerializeField] private InputActionReference moveAction;

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
        linesPosition = new Transform[lineContainer.childCount];
        for (int i = 0; i < lineContainer.childCount; i++)
        {
            linesPosition[i] = lineContainer.GetChild(i);
        }
        actualPosition = linesPosition[lineIndex];
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, actualPosition.position, playerHorizontalSpeed * Time.deltaTime);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        float inputMovement = context.ReadValue<float>();

        if (inputMovement < 0f)
        {
            if (lineIndex > 0)
            {
                lineIndex--;
            }
        }
        else if (inputMovement > 0f)
        {
            if (lineIndex < linesPosition.Length - 1)
            {
                lineIndex++;
            }
        }
        actualPosition = linesPosition[lineIndex];
    }
}
