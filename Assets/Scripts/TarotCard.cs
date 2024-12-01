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
    [SerializeField]
    private float flipDuration = 0.4f;

    [Header("References")]
    [SerializeField]
    private TextMeshPro cardText;

    private CardSlot cardData;
    private SpriteRenderer spriteRenderer;
    private bool isFaceDown = false;
    private bool isFlipping = false;
    private Action onClick;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipCard(int repeat = 1, Action onFlipFinished = null)
    {
        if (!isFlipping) StartCoroutine(Flip(repeat, onFlipFinished));
    }

    public void InitiateCard(CardSlot cardData, Action callBack = null, bool faceDown = false)
    {
        this.cardData = cardData;
        cardText.text = cardData.Content;
        onClick = callBack;

        if (faceDown)
        {
            isFaceDown = true;
            spriteRenderer.sprite = backSprite;
        }
        else {
            isFaceDown = false;
            spriteRenderer.sprite = faceSprite;
        }
    }

    // Flip the card 180 degrees and change the sprite. Repeat the process if needed.
    // An odd number of repeats will end with the card in the other way.
    // An even number of repeats will end with the card in the same way.
    private IEnumerator Flip(int repeat = 1, Action onFlipFinished = null)
    {
        isFlipping = true;

        for (int i = 0; i < repeat; i++)
        {
            float time = 0;
            float halfDuration = flipDuration / 2;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0, 180, 0) * startRotation;

            while (time < halfDuration)
            {
                time += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / halfDuration);
                yield return null;
            }

            spriteRenderer.sprite = isFaceDown ? faceSprite : backSprite;
            isFaceDown = !isFaceDown;

            time = 0;
            startRotation = transform.rotation;
            endRotation = Quaternion.Euler(0, 180, 0) * startRotation;

            while (time < halfDuration)
            {
                time += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / halfDuration);
                yield return null;
            }
        }

        onFlipFinished?.Invoke();
    }

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
