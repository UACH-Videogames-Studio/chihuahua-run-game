using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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
            gameOverPanel.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        // Get the current scene name 
        string currentScene = SceneManager.GetActiveScene().name;

        // Verify if enemyDataMap contain the current scene 
        if (enemyDataMap.ContainsKey(currentScene))
        {
            EnemyGameOverData enemyData = enemyDataMap[currentScene];

            // Configure the enemy image
            if (enemyImage != null && enemyData.enemySprite != null)
            {
                enemyImage.texture = enemyData.enemySprite;
                PlanchadaAnimation();
            }

            // Select a random phrase to enemies data and assign it to the phrase text
            if (phraseText != null && enemyData.phrases != null && enemyData.phrases.Count > 0)
            {
                int randomIndex = Random.Range(0, enemyData.phrases.Count);
                phraseText.text = enemyData.phrases[randomIndex];
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

    public void PlanchadaAnimation()
    {
        enemyImage.uvRect = new Rect(0.5f, 0, 1, 1);
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
