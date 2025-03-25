using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "FirstMinigame/DialogueSystem/ScriptableObjects/DialogueRound")]
public class DialogueRoundSO : ScriptableObject
{
    [SerializeField] public List<DialogueTurn> dialogueTurnsList;

    public List<DialogueTurn> DialogueTurnsList => dialogueTurnsList;
}
