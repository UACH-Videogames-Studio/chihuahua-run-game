using UnityEngine;

[System.Serializable]
public class DialogueTurn
{
    [SerializeField] public DialogueCharacterSO Character;
    [SerializeField, TextArea(2, 4)] private string dialogueLine = string.Empty;

    public string DialogueLine => dialogueLine;
}
