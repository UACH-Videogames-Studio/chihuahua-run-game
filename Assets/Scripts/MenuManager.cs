using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameNameScene;
    public void StartGame()
    {
        SceneManager.LoadScene(gameNameScene);
    }
    public void QuitGame()
    {
        // The #if is a special function in C# that activates itself before the build, so if we are in the editor, it quit the game mode, else, it quit the game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
           Application.Quit();
        #endif
    }
}