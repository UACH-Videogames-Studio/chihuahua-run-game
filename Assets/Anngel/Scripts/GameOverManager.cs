using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGameOverData
    {
        public Texture enemySprite;
        //A raw image is used as a container for each enemy's sprite.
        //A raw image cannot be assigned a Sprite data type, but rather a Texture type.
        public List<string> phrases;
    }

    [Header("UI References")]
    public GameObject gameOverPanel;
    public RawImage enemyImage; //Will be the container for the enemy's sprite.
    public TMP_Text phraseText;
    [SerializeField] private DialogueLevelStarter dialogueLevelStarter;
    private DialogueCharacterSO character;

    [Header("Animation UI Settings")]
    [SerializeField] private float duration = 0.75f;
    [Range(0.1f, 10f)][SerializeField] private float animationSpeed = 1.0f;

    [Header("Enemies Data")]
    public EnemyGameOverData planchadaData;
    public EnemyGameOverData sinowiData;
    public EnemyGameOverData pascualitaData;

    private Dictionary<string, EnemyGameOverData> enemyDataMap;

    void Awake()
    {
        // Initialize to data dictionary
        // mapping scene names to enemy data
        enemyDataMap = new Dictionary<string, EnemyGameOverData>()
        {
            {"Level1", planchadaData},
            {"Level2", sinowiData},
            {"Level3", pascualitaData}
        };

        // The panel must be disabled at start
        if (gameOverPanel != null)
        {
            //gameOverPanel.SetActive(false); //QUITAAR IMPORTANT!!!!
        }
    }

    public void ShowGameOver()
    {
        // Get the current scene name 
        string currentScene = SceneManager.GetActiveScene().name;

        // Verify if enemyDataMap contain the current scene 
        if (enemyDataMap.ContainsKey(currentScene))
        {
            if (dialogueLevelStarter == null)
            {
                Debug.LogWarning("DialogueLevelStarter is not assigned in the inspector.");
                return;
            }

            // character = dialogueLevelStarter.
            // Configure the enemy image
            if (enemyImage != null)
            {
                //enemyImage.texture = enemyData.enemySprite;
                StartCoroutine(PlanchadaAnimation());
            }

        }
        else
        {
            Debug.LogWarning($"No hay frases o imágenes configuradas para los enemigos: {currentScene}");
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

            yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0.6f, 0, 1, 1), adjustedDuration);
            yield return new WaitForSeconds(4.0f);
            yield return LerpUVRect(new Rect(0.6f, 0, 1, 1), new Rect(-0.6f, 0, 1, 1), 5.5f);
            yield return new WaitForSeconds(4.0f);
            yield return LerpUVRect(new Rect(-0.6f, 0, 1, 1), new Rect(0, 0, 1, 1), adjustedDuration);
            yield return LerpUVRect(new Rect(0, 0, 1, 1), new Rect(0, 0, 1, 1.5f), adjustedDuration);
            yield return LerpUVRect(new Rect(0, 0, 1, 1.5f), new Rect(0, 0, 1, -0.05f), adjustedDuration);
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
        }
    }
}
