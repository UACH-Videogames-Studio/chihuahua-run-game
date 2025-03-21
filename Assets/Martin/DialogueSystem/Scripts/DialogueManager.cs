using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI $ Typing")]
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private AudioSource typingAudioSource;

    [Header("InputAction")]
    [SerializeField] private InputActionReference nextDialogueActionRef;

    private Queue<DialogueTurn> dialogueTurnsQueue;
    private string currentTutorialKey;
    private bool skipTyping;
    public bool IsDialogueInProgress { get; private set; } = false;
    

    

    private void Awake()
    {
        Instance = this;
        dialogueUI.HideDialogueBox();
    }

    private void OnEnable()
    {
        if (nextDialogueActionRef != null)
        {
            nextDialogueActionRef.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (nextDialogueActionRef != null)
        {
            nextDialogueActionRef.action.Disable();
        }
    }

    public void StartDialogue(DialogueRoundSO dialogue, string tutorialKey = null)
    {
        if (IsDialogueInProgress)
        {
            Debug.Log($"Dialogue is already in progress");
            return;
        }

        currentTutorialKey = tutorialKey;

        IsDialogueInProgress = true;
        dialogueTurnsQueue = new Queue<DialogueTurn>(dialogue.DialogueTurnsList);
        StartCoroutine(DialogueCoroutine());

    }

    private IEnumerator DialogueCoroutine()
    {
        dialogueUI.ShowDialogueBox();
        while ( dialogueTurnsQueue.Count > 0)
        {
            var CurrentTurn = dialogueTurnsQueue.Dequeue();
            dialogueUI.SetCharacterInfo(CurrentTurn.Character);
            dialogueUI.ClearDialogueArea();

            yield return StartCoroutine(TypeSentence(CurrentTurn.DialogueLine));
            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitUntil(() => !nextDialogueActionRef.action.IsPressed());
            yield return new WaitUntil(() => nextDialogueActionRef.action.triggered);
            yield return null;
        }
        
        dialogueUI.HideDialogueBox();
        IsDialogueInProgress = false;

        if (!string.IsNullOrEmpty(currentTutorialKey))
        {
            PlayerPrefs.SetInt(currentTutorialKey, 1);
            PlayerPrefs.Save();
        }

        currentTutorialKey = null;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueUI.ClearDialogueArea();
        skipTyping = false;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (nextDialogueActionRef.action.triggered)
            {
                skipTyping = true;
            }
            if (skipTyping)
            {
                dialogueUI.AppendToDialogueArea(sentence.Substring(i));
                yield break;
            }

            char c = sentence[i];
            dialogueUI.AppendToDialogueArea(c);
            if (!char.IsWhiteSpace(c) && typingAudioSource != null)
                typingAudioSource.Play();

            float timer = 0f;
            while (timer < typingSpeed)
            {
                if (nextDialogueActionRef.action.triggered)
                {
                    skipTyping = true;
                    dialogueUI.AppendToDialogueArea(sentence.Substring(i + 1));
                    yield break;
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }


    //private IEnumerator TypeSentence(DialogueTurn dialogueTurn)
    //{
    //    var typingWaitSeconds = new WaitForSeconds(typingSpeed);

    //    foreach (char letter in dialogueTurn.DialogueLine.ToCharArray())
    //    {
    //        dialogueUI.AppendToDialogueArea(letter);
    //        if (!char.IsWhiteSpace(letter))
    //        {
    //            typingAudioSource.Play();
    //        }
    //        yield return typingWaitSeconds;
    //    }
    //}


}
