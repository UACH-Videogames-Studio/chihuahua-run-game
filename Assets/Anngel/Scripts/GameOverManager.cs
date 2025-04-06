using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }
    public class EnemyAnimationData
    {
        public IEnumerator animationCoroutine;
    }

    [Header("UI References")]
    public GameObject gameOverPanel;
    public RawImage enemyImage; //Will be the container for the enemy's sprite.
    [SerializeField] private DialogueLevelStarter dialogueLevelStarter;
    private DialogueCharacterSO character;

    [Header("Animation UI Settings")]
    [SerializeField] private float duration = 0.75f;
    [Range(0.1f, 10f)][SerializeField] private float animationSpeed = 1.0f;

    [Header("Enemies Animations")]
    public EnemyAnimationData planchadaAnimation;
    public EnemyAnimationData sinowiAnimation;
    public EnemyAnimationData pascualitaAnimation;

    private Dictionary<string, EnemyAnimationData> enemyAnimationsMap;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
        }
        // create new instances of EnemyAnimationData if they are null
        planchadaAnimation ??= new EnemyAnimationData();
        sinowiAnimation ??= new EnemyAnimationData();
        pascualitaAnimation ??= new EnemyAnimationData();
        // is equivalent to: 
        // if (planchadaAnimation == null)
        // {
        //     planchadaAnimation = new EnemyAnimationData();
        // }

        // Initialize animation coroutines for each enemy animation data instance
        planchadaAnimation.animationCoroutine = PlanchadaAnimation();
        sinowiAnimation.animationCoroutine = SinowiAnimation();
        pascualitaAnimation.animationCoroutine = PlanchadaAnimation();

        // Initialize to data dictionary
        // mapping scene names to enemy data animations
        enemyAnimationsMap = new Dictionary<string, EnemyAnimationData>()
        {
            {"Level1", planchadaAnimation},
            {"Level2", sinowiAnimation},
            {"Level3", pascualitaAnimation}
        };

        // The panel must be disabled at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (dialogueLevelStarter == null)
        {
            Debug.LogWarning("DialogueLevelStarter is not assigned in the inspector.");
            return;
        }

        // Get the current scene name 
        string currentScene = SceneManager.GetActiveScene().name;

        if (enemyAnimationsMap.TryGetValue(currentScene, out EnemyAnimationData enemyData))
        {
            if (enemyData == null || enemyData.animationCoroutine == null)
            {
                Debug.LogError($"Datos de animación no configurados para {currentScene}");
                return;
            }

            character = dialogueLevelStarter.SelectCharacter();

            if (character != null && enemyImage != null)
            {
                enemyImage.texture = character.charcaterSprite;
            }

            StartCoroutine(enemyData.animationCoroutine);
        }
        else
        {
            Debug.LogWarning($"No hay animación configurada para la escena: {currentScene}");
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public IEnumerator PlanchadaAnimation()
    {
        while (true)
        {
            float adjustedDuration = duration / animationSpeed;

            yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0.6f, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2.0f);
            yield return LerpUVRect(new Rect(0.6f, 0, 1, 1), new Rect(-0.6f, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2.0f);
            yield return LerpUVRect(new Rect(-0.6f, 0, 1, 1), new Rect(0, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2.0f);
            yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0, 0, 1, 1.5f), 2.0f);
            yield return LerpUVRect(new Rect(0, 0, 1, 1.5f), new Rect(0, 0, 1, -0.05f), 2.0f);
            yield return new WaitForSecondsRealtime(2.0f);
            yield return LerpUVRect(new Rect(0, 0, 1, -0.05f), new Rect(0, 0, 1, 1), 2.0f);
        }
    }

    public IEnumerator SinowiAnimation()
    {
        while (true)
        {
            float adjustedDuration = duration / animationSpeed;

            yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0.6f, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2f);
            yield return LerpUVRect(new Rect(0.6f, 0, 1, 1), new Rect(-0.6f, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2f);
            yield return LerpUVRect(new Rect(-0.6f, 0, 1, 1), new Rect(0, 0, 1, 1), 2.0f);
            yield return new WaitForSecondsRealtime(2f);
            // yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0, 0, 1, 1.5f), adjustedDuration);
            // yield return LerpUVRect(new Rect(0, 0, 1, 1.5f), new Rect(0, 0, 1, -0.05f), adjustedDuration);
        }
    }

    private IEnumerator LerpUVRect(Rect start, Rect end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration); //Normalize time to [0, 1]
            //if time is greater than duration, set t to 1
            enemyImage.uvRect = new Rect(
                Mathf.Lerp(start.x, end.x, t),
                Mathf.Lerp(start.y, end.y, t),
                Mathf.Lerp(start.width, end.width, t),
                Mathf.Lerp(start.height, end.height, t)
            );
            yield return null;
        }
        enemyImage.uvRect = end;
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        // For testing purposes, show the game over screen when pressing the "T" key
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowGameOver();
            dialogueLevelStarter.CallDialogue();
        }
    }

    public void GameOver()
    {
        ShowGameOver();
        dialogueLevelStarter.CallDialogue();
    }
}
