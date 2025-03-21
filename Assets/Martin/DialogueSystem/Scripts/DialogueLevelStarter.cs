using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueLevelStarter : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;
    //[SerializeField] private string playerPrefsKey = string.Empty;

    private void Start()
    {
        
        if (!DialogueManager.Instance.IsDialogueInProgress)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string roundName = dialogue != null ? dialogue.name : "UnknownDialogue";
            string key = "HasShownTutorial_" + sceneName + "_" + roundName;

            bool wasTutorialShown = PlayerPrefs.GetInt(key, 0) == 1;
            if (!wasTutorialShown)
            {
                DialogueManager.Instance.StartDialogue(dialogue, key);
            }
        }
    }
}
