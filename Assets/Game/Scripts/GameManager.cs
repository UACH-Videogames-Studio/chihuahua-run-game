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
    [HideInInspector] public bool isPlay;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    private void Awake()
    {   
        //pausePannel.SetActive(false);
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
        //gamePannel.SetActive(true);
        
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
        isPlay = true;
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

        //gamePannel.SetActive(false);
        gameCanvasGroup.alpha = 0f;
        gameCanvasGroup.interactable = false;
        gameCanvasGroup.blocksRaycasts = false;
        //pausePannel.SetActive(true);
        pauseCanvasGroup.alpha = 1f;
        pauseCanvasGroup.interactable = true;
        pauseCanvasGroup.blocksRaycasts = true;
        //SetInputMap("UI");
        ChangeToUIActions();
        isPlay = false;
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
        //pausePannel.SetActive(false);
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        //gamePannel.SetActive(true);
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
        //SetInputMap("Base");
        ChangeToBaseActions();
        isPlay = true;
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