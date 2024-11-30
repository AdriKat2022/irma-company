using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private CustomerData customerData;
    private Dialogue dialogue;

    private Queue<DialogueWave> dialogueWaves;
    private Queue<string> sentences;
    private Queue<float> nextLineDelays;
    private Queue<bool> isPlayerTalkings;

    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator animator;
    void Start()
    {
        dialogue = customerData.Dialogue;
        dialogueWaves = new Queue<DialogueWave>();
        sentences = new Queue<string>();
        isPlayerTalkings = new Queue<bool>();
        nextLineDelays = new Queue<float>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        animator.SetBool("isOpen", true);
        characterNameText.text = customerData.Name;
        dialogueWaves.Clear();
        sentences.Clear();
        nextLineDelays.Clear();
        isPlayerTalkings.Clear();

        foreach (DialogueWave dialogueWave in dialogue.DialogueWaves) 
        { 
            foreach (DialogueLine dialogueLine in dialogueWave.Lines)
            {
                sentences.Enqueue(dialogueLine.Text);
                nextLineDelays.Enqueue(dialogueLine.NextLineDelay);
                isPlayerTalkings.Enqueue(dialogueLine.isPlayerTalking);
            }
        }
        StartCoroutine(OpenDialogue());
    }

    IEnumerator OpenDialogue()
    {
        yield return new WaitForSeconds(2);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0) 
        { 
            string sentence = sentences.Dequeue();
            float nextLineDelay = nextLineDelays.Dequeue();
            bool isPlayerTalking = isPlayerTalkings.Dequeue();
            if (isPlayerTalking) { characterNameText.text = "Player"; } else { characterNameText.text = customerData.Name; }
            StartCoroutine(TypeSentence(sentence, nextLineDelay));
        }
        else 
        {
            EndDialogue();
            return;
        }
    }

    IEnumerator TypeSentence(string sentence, float nextLineDelay)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(nextLineDelay);
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
