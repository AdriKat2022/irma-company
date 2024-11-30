using System;
using UnityEngine;


/// <summary>
/// Smallest unit of dialogue.
/// [Text] is the text that will be displayed in the dialogue box.
/// [AutoTiming] is the time to wait before going to the next line if any. This is useful for auto-timed dialogues.
/// </summary>
[Serializable]
public struct DialogueLine
{
    public string Text;
    public bool isPlayerTalking;
    public float NextLineDelay;
}

/// <summary>
/// [Lines] is the array of lines that will be displayed in the dialogue box.
/// [AudioFile] is the audio file that will be played when the dialogue is displayed.
/// </summary>
[Serializable]
public struct DialogueWave
{
    public AudioClip AudioFile;
    public DialogueLine[] Lines;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [field: SerializeField]
    public DialogueWave[] DialogueWaves { get; private set; }
}
