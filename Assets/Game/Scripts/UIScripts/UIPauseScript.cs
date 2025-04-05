using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseScript : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager sceneTransitionManager;
    public void Resume()
    {
        GameManager.Instance.TryResumeGame();
    }
    public void ChangeScene()
    {
        Time.timeScale = 1;
        sceneTransitionManager.LoadNextScene();
    }
}
