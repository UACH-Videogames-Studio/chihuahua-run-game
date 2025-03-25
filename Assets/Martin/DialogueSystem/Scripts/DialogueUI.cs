using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    //Dialogue UI Elements
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private Image CharacterPhoto;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI dialogueArea;

    public void ShowDialogueBox()
    {
        dialogueBox.gameObject.SetActive(true);
    }

    public void HideDialogueBox()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void SetCharacterInfo(DialogueCharacterSO character)
    {
        if (CharacterName == null)
        {
            CharacterName.text = character.name;
            CharacterPhoto.sprite = character.ProfilePhoto;
        }
    }

    public void ClearDialogueArea()
    {
        dialogueArea.text = string.Empty;
    }

    public void SetDialogueArea(string text)
    {
        dialogueArea.text = text;
    }

    public void AppendToDialogueArea(char letter)
    {
        dialogueArea.text += letter;
    }

    public void AppendToDialogueArea(string text)
    {
        dialogueArea.text += text;
    }
}
