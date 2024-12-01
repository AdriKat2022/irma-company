using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TarotCard : MonoBehaviour
{
    public CardSlot CardData => cardData;

    [SerializeField]
    private Sprite faceSprite;
    [SerializeField]
    private Sprite backSprite;

    [Header("References")]
    [SerializeField]
    private TextMeshPro cardText;
    [SerializeField]
    private CardFlipper cardFlipper;

    private CardSlot cardData;
    private Action onClick;

    public void FlipCard(int repeat = 1, Action onFlipFinished = null)
    {
        StartCoroutine(cardFlipper.Flip(repeat, onFlipFinished));
    }

    public void InitiateCard(CardSlot cardData, Action callBack = null, bool faceDown = false)
    {
        Debug.Log("initialized", gameObject);
        this.cardData = cardData;
        cardText.text = cardData.Content;
        onClick = callBack;
        cardFlipper.SetFaceDown(faceDown);
    }

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
