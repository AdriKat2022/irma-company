using System;
using UnityEngine;

[Serializable]
public struct CardSlot
{
    public string Content;
    public int CharacterScore;
}

[Serializable]
public struct Question
{
    public string InitialSentence;
    public CardSlot[] AvailableCards;
}

[CreateAssetMenu(fileName = "NewCustomerCharacter", menuName = "Customer Character")]
public class CustomerData : ScriptableObject
{
    [field: Header("Character Looks")]
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public Sprite CharacterSprite { get; private set; }

    [field: Header("Dialogue")]
    [field: SerializeField]
    public Dialogue Dialogue { get; private set; }

    [field: Header("Prediction")]
    [field: SerializeField]
    public Question[] Questions { get; private set; }
}
