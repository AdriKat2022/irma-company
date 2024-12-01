using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string playerName = "Player";

    [SerializeField] private CustomerData customerData;
    private Dialogue dialogue;

    private bool isDialogueActive = false;

    private Queue<DialogueWave> dialogueWaves;
    private Queue<string> sentences;
    private Queue<float> nextLineDelays;
    private Queue<bool> isPlayerTalkings;
    private Queue<AudioClip> audioFiles;

    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator animator;

    void Start()
    {
        dialogueWaves = new Queue<DialogueWave>();
        sentences = new Queue<string>();
        isPlayerTalkings = new Queue<bool>();
        nextLineDelays = new Queue<float>();
        audioFiles = new Queue<AudioClip>();
        dialogue = customerData.Dialogue;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartDialogue(() => print("Dialogue ended")) ;
        }
    }

    public void SetNewDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;

        dialogueWaves.Clear();
        sentences.Clear();
        nextLineDelays.Clear();
        isPlayerTalkings.Clear();
    }

    public void SetNewCustomer(CustomerData customerData)
    {
        this.customerData = customerData;
        dialogue = customerData.Dialogue;
        characterNameText.text = customerData.Name;

        dialogueWaves.Clear();
        sentences.Clear();
        nextLineDelays.Clear();
        isPlayerTalkings.Clear();
    }

    public void StartDialogue(Action onDialogueComplete = null)
    {
        if (isDialogueActive || dialogue == null)
        {
            Debug.LogError("Dialogue is already active or not set");
            return;
        }

        isDialogueActive = true;

        animator.SetBool("isOpen", true);

        foreach (DialogueWave dialogueWave in dialogue.DialogueWaves) 
        { 
            foreach (DialogueLine dialogueLine in dialogueWave.Lines)
            {
                sentences.Enqueue(dialogueLine.Text);
                nextLineDelays.Enqueue(dialogueLine.NextLineDelay);
                isPlayerTalkings.Enqueue(dialogueLine.isPlayerTalking);
                audioFiles.Enqueue(dialogueLine.AudioFile);
            }
        }

        StartCoroutine(OpenDialogue(onDialogueComplete));
    }

    IEnumerator OpenDialogue(Action onDialogueComplete = null)
    {
        yield return new WaitForSeconds(1.5f);
        DisplayNextSentence(onDialogueComplete);
    }

    public void DisplayNextSentence(Action onDialogueComplete = null)
    {
        if (sentences.Count > 0) 
        { 
            string sentence = sentences.Dequeue();
            float nextLineDelay = nextLineDelays.Dequeue();
            bool isPlayerTalking = isPlayerTalkings.Dequeue();
            AudioClip audioFile = audioFiles.Dequeue();
            if (isPlayerTalking) { characterNameText.text = playerName; } else { characterNameText.text = customerData.Name; }
            StartCoroutine(TypeSentence(sentence, nextLineDelay, audioFile, onDialogueComplete));
        }
        else 
        {
            EndDialogue(onDialogueComplete);
            return;
        }
    }

    IEnumerator TypeSentence(string sentence, float nextLineDelay, AudioClip audioFile, Action onDialogueComplete = null)
    {
        AudioManager.Instance.PlayVoice(audioFile);

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(nextLineDelay);
        DisplayNextSentence(onDialogueComplete);
    }

    public void EndDialogue(Action onDialogueComplete = null)
    {
        dialogueText.text = "";
        animator.SetBool("isOpen", false);
        isDialogueActive = false;
        onDialogueComplete?.Invoke();
    }
}
