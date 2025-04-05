using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialoguePair
{
    public string sceneName;
    public DialogueCharacterSO character;
}
public class DialogueLevelStarter : MonoBehaviour
{
    //[SerializeField] private DialogueRoundSO dialogue;
    //[SerializeField] private DialogueCharacterSO[] characters;
    [SerializeField] private List<DialoguePair> dialoguePairs = new List<DialoguePair>();
    [SerializeField] private Dictionary<string, DialogueCharacterSO> charactersDictionary = new Dictionary<string, DialogueCharacterSO>();
    //[SerializeField] private string playerPrefsKey = string.Empty;

    private void Awake()
    {
        charactersDictionary = new Dictionary<string, DialogueCharacterSO>();
        foreach(DialoguePair pair in dialoguePairs)
        {
            if (!charactersDictionary.ContainsKey(pair.sceneName))
            {
                charactersDictionary.Add(pair.sceneName, pair.character);
            }
            else
            {
                Debug.LogWarning($"Duplicate scene name found: {pair.sceneName}. Please ensure unique scene names.");
            }
        }

    }
    private void Start()
    {

        //if (!DialogueManager.Instance.IsDialogueInProgress)
        //{

        //    //string sceneName = SceneManager.GetActiveScene().name;
        //    //string roundName = dialogue != null ? dialogue.name : "UnknownDialogue";
        //    //string key = "HasShownTutorial_" + sceneName + "_" + roundName;


        //    DialogueRoundSO dialogue = SelectDialogueFromCharacter();
        //    string key = CreateKey(dialogue);
        //    bool wasTutorialShown = PlayerPrefs.GetInt(key, 0) == 1;
        //    if (!wasTutorialShown)
        //    {
        //        DialogueManager.Instance.StartDialogue(dialogue, key);
        //    }

        //}
        //CallDialogue();
    }

    public DialogueCharacterSO SelectCharacter()
    {
        //if (characters.Length < 0)
        //{
        //    Debug.LogWarning("No characters available to select.");
        //    return null;
        //}
        //return Random.Range(0, characters.Length);
        string sceneName = SceneManager.GetActiveScene().name;
        if (charactersDictionary.ContainsKey(sceneName))
        {
            return charactersDictionary[sceneName];
        }
        return null;



    }
    private DialogueRoundSO SelectDialogueFromCharacter(DialogueCharacterSO selectedCharacter)
    {
        //int characterIndex = SelectCharacter();
        //if (characterIndex < 0)
        //{
        //    return null; 
        //}
        //DialogueCharacterSO selectedCharacter = SelectCharacter();
        //DialogueCharacterSO selectedCharacter = characters[characterIndex];
        int dialogueIndex = Random.Range(0, selectedCharacter.dialogues.Length);
        return selectedCharacter.dialogues[dialogueIndex];
    }

    private string CreateKey(DialogueRoundSO dialogue)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string roundName = dialogue != null ? dialogue.name : "UnknownDialogue";
        return "HasShownTutorial_" + sceneName + "_" + roundName;
    }

    public void CallDialogue()
    {
        if (DialogueManager.Instance.IsDialogueInProgress)
        {
            Debug.Log($"Dialogue is already in progress");
            return;
        }
        DialogueCharacterSO selectedCharacter = SelectCharacter();
        Debug.Log($"Selected character: {selectedCharacter.Name}");
        DialogueRoundSO dialogue = SelectDialogueFromCharacter(selectedCharacter);
        string key = CreateKey(dialogue);
        bool wasTutorialShown = PlayerPrefs.GetInt(key, 0) == 1;
        if (!wasTutorialShown)
        {
            DialogueManager.Instance.StartDialogue(dialogue, key);
        }
    }
}
