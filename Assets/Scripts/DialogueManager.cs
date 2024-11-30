using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private CustomerData customerData;

    private Queue<DialogueWave> dialogueWaves;
    private Queue<string> sentences;
    private Queue<float> nextLineDelays;

    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    void Start()
    {
        Dialogue dialogue = customerData.Dialogue;
        dialogueWaves = new Queue<DialogueWave>();
        sentences = new Queue<string>();
        nextLineDelays = new Queue<float>();
        StartDialogue(dialogue);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space");
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        characterNameText.text = customerData.Name;
        dialogueWaves.Clear();
        sentences.Clear();
        nextLineDelays.Clear();

        foreach (DialogueWave dialogueWave in dialogue.DialogueWaves) 
        { 
            foreach (DialogueLine dialogueLine in dialogueWave.Lines)
            {
                sentences.Enqueue(dialogueLine.Text);
                nextLineDelays.Enqueue(dialogueLine.NextLineDelay);
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0) 
        { 
            string sentence = sentences.Dequeue();
            float nextLineDelay = nextLineDelays.Dequeue();
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
        Debug.Log("Le dialogue est terminée");
    }
}
