using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("The variables to assign")][Space(10)]
    [SerializeField][Tooltip("Pause panel in PlayerCanvas")] private GameObject pausePannel;
    [SerializeField][Tooltip("Game panel in PlayerCanvas")] private GameObject gamePannel;
    [SerializeField][Tooltip("Win panel in PlayerCanvas")] private GameObject winPannel;
    [SerializeField][Tooltip("Lose panel in PlayerCanvas")] private GameObject losePannel;
    [SerializeField][Tooltip("Game canvas group")] private CanvasGroup gameCanvasGroup;
    [SerializeField][Tooltip("Pause canvas group")] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private PannelsScriptableObjects scriptableObjects;
    [HideInInspector] public float timeMultiplier;
    [HideInInspector] public bool isPlay;
    [SerializeField] private PlayerMovementScript playerScript;
    public PlayerMovementScript PlayerScript
    {
        get
        {
            if (playerScript == null)
            {
                playerScript = PlayerMovementScript.Instance;
            }
            return playerScript;
        }
    }
    //public void SetPanels()
    //{
    //    //pausePannel = pannelsScriptableObjects.PausePannel;
    //    //gamePannel = pannelsScriptableObjects.GamePannel;
    //    //winPannel = pannelsScriptableObjects.WinPannel;
    //    //losePannel = pannelsScriptableObjects.LosePannel;
    //    GameObject.Find("PausePannel");
    //    GameObject.Find("GamePannel");
    //    GameObject.Find("WinPannel");
    //    GameObject.Find("LosePannel");
    //    gameCanvasGroup = gamePannel.GetComponent<CanvasGroup>();
    //    pauseCanvasGroup = pausePannel.GetComponent<CanvasGroup>();
    //    timeMultiplier = 1;
    //    isPlay = true;
    //    winPannel.SetActive(false);
    //    losePannel.SetActive(false);
    //    pauseCanvasGroup.alpha = 0f;
    //    pauseCanvasGroup.interactable = false;
    //    pauseCanvasGroup.blocksRaycasts = false;
    //    gameCanvasGroup.alpha = 1f;
    //    gameCanvasGroup.interactable = true;
    //    gameCanvasGroup.blocksRaycasts = true;
    //}

    public void SetPanels(
     GameObject pausePanelObj,
     GameObject gamePanelObj,
     GameObject winPanelObj,
     GameObject losePanelObj)
    {
        pausePannel = pausePanelObj;
        gamePannel = gamePanelObj;
        winPannel = winPanelObj;
        losePannel = losePanelObj;

        // Y obtienes los CanvasGroup desde cada objeto, si lo deseas
        pauseCanvasGroup = pausePannel.GetComponent<CanvasGroup>();
        gameCanvasGroup = gamePannel.GetComponent<CanvasGroup>();
        // ...

        // Y pones aquí el “reset” del estado de la UI
        Time.timeScale = 1;
        isPlay = true;
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
        winPannel.SetActive(false);
        losePannel.SetActive(false);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; 
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
        isPlay = true;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        timeMultiplier = 1;
        isPlay = true;
        winPannel.SetActive(false);
        losePannel.SetActive(false);
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => 
        PlayerMovementScript.Instance != null && 
        PlayerMovementScript.Instance.inputActions != null);
    
        playerScript = PlayerMovementScript.Instance;
    
        
        if(playerScript.inputActions != null)
        {
            playerScript.inputActions.Base.Pause.started += PauseGame;
            playerScript.inputActions.UI.Resume.started += ResumeGame;
            playerScript.inputActions.Enable();
        }
    }
    private void OnEnable()
    {
        var allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (var t in allTransforms)
        {
            if (t.name == "PausePanel" && t.gameObject.scene.name == SceneManager.GetActiveScene().name)
            {
                // t es el transform del panel inactivo en la escena actual
            }
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (Instance != this) return;
        if (PlayerMovementScript.Instance == null) return;
        if (PlayerScript.inputActions != null)
        {
           PlayerScript.inputActions.Base.Pause.started -= PauseGame;
            PlayerScript.inputActions.UI.Resume.started -= ResumeGame;
            PlayerScript.inputActions.Disable(); 
        }
        
    }
    public void ChangeToBaseActions()
    {
        PlayerScript.inputActions.Disable();
        PlayerScript.inputActions.Base.Enable();
    }
    public void ChangeToUIActions()
    {
        PlayerScript.inputActions.Disable();
        PlayerScript.inputActions.UI.Enable();
    }
    private void PauseGame(InputAction.CallbackContext context)
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

        pauseCanvasGroup.alpha = 1f;
        pauseCanvasGroup.interactable = true;
        pauseCanvasGroup.blocksRaycasts = true;

        gameCanvasGroup.alpha = 0f;
        gameCanvasGroup.interactable = false;
        gameCanvasGroup.blocksRaycasts = false;

        ChangeToUIActions();
        isPlay = false;
        Debug.Log("The game was paused");
    }
    private void ResumeGame(InputAction.CallbackContext context)
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
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;
        gameCanvasGroup.interactable = true;
        gameCanvasGroup.blocksRaycasts = true;
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
    public void WinGame()
    {
        winPannel.SetActive(true);
    }
    public void LoseGame()
    {
        GameOverManager.Instance.GameOver();
    }
}