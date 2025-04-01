using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/DialogueSystem/DialogueRound")]
public class DialogueRoundSO : ScriptableObject
{
    [SerializeField] public List<DialogueTurn> dialogueTurnsList;

    public List<DialogueTurn> DialogueTurnsList => dialogueTurnsList;
}
