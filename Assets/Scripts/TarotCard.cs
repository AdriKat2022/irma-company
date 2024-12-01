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

    private IEnumerator Flip(int repeat = 1, Action onFlipFinished = null)
    {
        isFlipping = true;

        for (int i = 0; i < repeat; i++)
        {
            float time = 0;
            Vector3 scale = transform.localScale;
            Vector3 targetScale = new Vector3(0, scale.y, scale.z);

            while (time < flipDuration)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(scale, targetScale, time / flipDuration);
                yield return null;
            }

            spriteRenderer.sprite = isFaceDown ? faceSprite : backSprite;
            isFaceDown = !isFaceDown;

            time = 0;
            scale = transform.localScale;
            targetScale = new Vector3(1, scale.y, scale.z);

            while (time < flipDuration)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(scale, targetScale, time / flipDuration);
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
