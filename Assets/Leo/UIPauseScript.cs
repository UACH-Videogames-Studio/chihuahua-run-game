using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseScript : MonoBehaviour
{
    [SerializeField] private string newScene;
    public void Resume()
    {
        GameManager.Instance.TryResumeGame();
    }
    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(newScene);
    }
}
