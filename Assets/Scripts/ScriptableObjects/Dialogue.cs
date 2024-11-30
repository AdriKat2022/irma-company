using UnityEngine;

public struct DialogueLine
{
    public string Content;
    public float autoTime;
}

public class Dialogue : ScriptableObject
{
    [field: SerializeField]
    public string[] Content { get; private set; }
}
