using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [Header("Canvas objects de ESTA escena")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        // Al iniciar ESTA escena, notificamos al GameManager (si existe)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPanels(pausePanel, gamePanel, winPanel, losePanel);
        }
    }
}
