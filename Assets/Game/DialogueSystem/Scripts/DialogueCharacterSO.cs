using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Character", menuName = "ScriptableObjects/DialogueSystem/DialogueCharacter")]
public class DialogueCharacterSO : ScriptableObject
{
    [Header("Character Info")]
    [SerializeField] public string characterName;
    //[SerializeField] private Sprite profilePhoto;
    [SerializeField] public DialogueRoundSO[] dialogues;
    [SerializeField] public Texture charcaterSprite;


    public string Name => characterName;
    //public Sprite ProfilePhoto => profilePhoto;
}
