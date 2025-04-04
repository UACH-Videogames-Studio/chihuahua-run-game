using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;} //This line is our public acces for all of codes
    [Header("Tha variables to assign")][Space(10)]
    [SerializeField][Tooltip("Here goes the pause pannel in PlayerCanvas")] private GameObject pausePannel;
    [SerializeField][Tooltip("Here goes the game pannel in PlayerCanvas")] private GameObject gamePannel;
    [SerializeField][Tooltip("Here goes the player")] private PlayerMovementScript playerScript;
    [HideInInspector] public float timeMultiplier;
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
        timeMultiplier = 1;
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
        PauseGame();
    }
    public void TryPauseGame()
    {
        PauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        gamePannel.SetActive(false);
        pausePannel.SetActive(true);
        //SetInputMap("UI");
        ChangeToUIActions();
        Debug.Log("The game was paused");
    }
    private void ResumeGame(InputAction.CallbackContext callbackContext)
    {
        ResumeGame();
    }
    public void TryResumeGame()
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
    public void ApplySlowDown()
    {
        timeMultiplier = 2;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.01f;
    }
    public void ApplyFastUp()
    {
        timeMultiplier = 1;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}