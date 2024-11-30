using System;
using UnityEngine;

[Serializable]
public struct CardSlot
{
    [Range(0, 10)]
    public int CharacterScore;
    public string Content;
}

[Serializable]
public struct Question
{
    public string InitialSentence;
    public CardSlot[] AvailableCards;
}

[Serializable]
public struct ReviewTreshold
{
    [Range(0, 10)]
    public int RequiredScore;
    public string ReviewText;
}

[CreateAssetMenu(fileName = "NewCustomerCharacter", menuName = "Customer Character")]
public class CustomerData : ScriptableObject
{
    [field: Header("Character Looks")]
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Sprite CharacterSprite { get; private set; }
    [field: SerializeField]
    public string Username { get; private set; }
    [field: SerializeField]
    public Sprite ProfilePicture { get; private set; }

    [field: Header("Dialogue")]
    [field: SerializeField]
    public Dialogue Dialogue { get; private set; }

    [field: Header("Prediction")]
    [field: SerializeField]
    public Question[] Questions { get; private set; }

    [field: SerializeField]
    public ReviewTreshold[] ReviewTresholds { get; private set; }
}
