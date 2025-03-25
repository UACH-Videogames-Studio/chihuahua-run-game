using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;
    [ContextMenu("Trigger Dialogue")]
    public void TriggerDialogue()
    {
        if (DialogueManager.Instance.IsDialogueInProgress)
            return;
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void ReplaceDialogue(DialogueRoundSO newDialog) => dialogue = newDialog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
