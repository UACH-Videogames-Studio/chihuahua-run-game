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
    [SerializeField] private GameObject gamePannel;
    [SerializeField] private PlayerMovementScript playerScript;
    private PenguinInputActions inputActions;
    private void Awake()
    {
        
        pausePannel.SetActive(false);
        gamePannel.SetActive(true);
        
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
        Time.timeScale = 0;
        gamePannel.SetActive(false);
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
        Time.timeScale = 1;
        pausePannel.SetActive(false);
        gamePannel.SetActive(true);
        //SetInputMap("Base");
        ChangeToBaseActions();
        Debug.Log("The game was continued");
    }
}
