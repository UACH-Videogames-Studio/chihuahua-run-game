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

    private PenguinInputActions inputActions;
    private void Awake()
    {
        inputActions = new PenguinInputActions();
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
        inputActions.UI.Disable();
        inputActions.Base.Enable();
    }
    public void ChangeToUIActions()
    {
        inputActions.Base.Disable();
        inputActions.UI.Enable();
    }
    private void OnEnable()
    {
        inputActions.Base.Pause.started += PauseGame;
        inputActions.UI.Resume.started += ResumeGame;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Base.Pause.started -= PauseGame;
        inputActions.UI.Resume.started -= ResumeGame;
        inputActions.Disable();
    }
    private void PauseGame(InputAction.CallbackContext callbackContext)
    {
        pausePannel.SetActive(true);
        SetInputMap("UI");
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
        SetInputMap("Base");
        Debug.Log("The game was continued");
    }
    private void SetInputMap(string mapName)
    {
        inputActions.Disable();

        if (mapName == "Base")
        {
            inputActions.Base.Enable();
        }
        else if (mapName == "UI")
        {
            inputActions.UI.Enable();
        }
    }
}
