using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Main menu")]
    //[SerializeField] private string gameNameScene;
    //[SerializeField] private GameObject optionsPanel;
    [SerializeField] private TextMeshProUGUI[] titleText;

    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private SceneTransitionManager sceneTransitionManager;

    [Header("Options menu")]
    [SerializeField] private CanvasGroup optionsCanvasGroup;

    public void StartGame()
    {
        sceneTransitionManager.LoadNextScene();
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

    public void OptionsButton()
    {
        optionsCanvasGroup.alpha = 1f;
        optionsCanvasGroup.interactable = true;
        optionsCanvasGroup.blocksRaycasts = true;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;
        foreach(TextMeshProUGUI text in titleText)
        {
            text.gameObject.SetActive(false);
        }
        //titleText.gameObject.SetActive(false);
    }

    public void QuitOptionsPanel()
    {
        optionsCanvasGroup.alpha = 0f;
        optionsCanvasGroup.interactable = false;
        optionsCanvasGroup.blocksRaycasts = false;
        mainMenuCanvasGroup.interactable = true;
        mainMenuCanvasGroup.blocksRaycasts = true;
        foreach (TextMeshProUGUI text in titleText)
        {
            text.gameObject.SetActive(true);
        }
        //titleText.gameObject.SetActive (true);
    }

}