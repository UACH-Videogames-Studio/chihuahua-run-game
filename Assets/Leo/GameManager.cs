using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;} //This line is our public acces for all of codes, if we need to change state we use something like: GameManager.Instance.ChangeState(GameManager.GameState.Playing)

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public GameState CurrentState { get; private set; } //With this line we can know what currentState is, example: if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
    [SerializeField] private GameObject pausePannel;

    [SerializeField] private PlayerMovementScript playerScript;
    private void Awake()
    {

        pausePannel.SetActive(false);
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeToBaseActions()
    {
        playerScript.inputActions.Disable();
        playerScript.inputActions.Base.Enable();
    }
    public void ChangeToUIActions()
    {
        playerScript.inputActions.Disable();
        playerScript.inputActions.UI.Enable();
    }
    private void OnEnable()
    {
        playerScript.inputActions.Base.Pause.started += PauseGame;
        playerScript.inputActions.UI.Resume.started += ResumeGame;
        playerScript.inputActions.Enable();
    }
    private void OnDisable()
    {
        playerScript.inputActions.Base.Pause.started -= PauseGame;
        playerScript.inputActions.UI.Resume.started -= ResumeGame;
        playerScript.inputActions.Disable();
    }
    private void PauseGame(InputAction.CallbackContext callbackContext)
    {
        pausePannel.SetActive(true);
        //SetInputMap("UI");
        ChangeToUIActions();
        Debug.Log("The game was paused");
    }
    public void TryResumeGame()
    {
        ResumeGame();
    }
    private void ResumeGame(InputAction.CallbackContext callbackContext)
    {
        ResumeGame();
    }
    private void ResumeGame()
    {
        pausePannel.SetActive(false);
        //SetInputMap("Base");
        ChangeToBaseActions();
        Debug.Log("The game was continued");
    }
}
