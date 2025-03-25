using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Character", menuName = "FirstMinigame/DialogueSystem/ScriptableObjects/DialogueCharacter")]
public class DialogueCharacterSO : ScriptableObject
{
    [Header("Character Info")]
    [SerializeField] private string characterName;
    [SerializeField] private Sprite profilePhoto;

    public string Name => characterName;
    public Sprite ProfilePhoto => profilePhoto;
}
