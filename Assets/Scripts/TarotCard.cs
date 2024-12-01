using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TarotCard : MonoBehaviour
{
    public CardSlot CardData => cardData;

    [SerializeField]
    private Sprite frontSprite;
    [SerializeField]
    private Sprite backSprite;
    [SerializeField]
    private float flipDuration = 0.4f;

    [Header("References")]
    [SerializeField]
    private TextMeshPro cardText;

    private CardSlot cardData;
    private SpriteRenderer spriteRenderer;
    private bool isFlipped = false;
    private bool isFlipping = false;
    private Action onClick;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipCard(int repeat = 1)
    {
        if (!isFlipping) StartCoroutine(Flip(repeat));
    }

    public void InitiateCard(CardSlot cardData, Action callBack = null, bool faceDown = false)
    {
        this.cardData = cardData;
        cardText.text = cardData.Content;
        onClick = callBack;

        if (faceDown)
        {
            isFlipped = true;
            spriteRenderer.sprite = backSprite;
            cardText.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            isFlipped = false;
            spriteRenderer.sprite = frontSprite;
            cardText.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator Flip(int repeat = 1)
    {
        isFlipping = true;

        for (int i = 0; i < repeat; i++)
        {
            float t = 0;
            float startAngle = isFlipped ? 90 : 0;
            float endAngle = isFlipped ? 0 : 90;
            spriteRenderer.sprite = isFlipped ? backSprite : frontSprite;
            cardText.gameObject.SetActive(isFlipped);
            isFlipped = !isFlipped;

            while (t < flipDuration)
            {
                t += Time.deltaTime;
                float angle = Mathf.Lerp(startAngle, endAngle, t / flipDuration);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }

        }

        cardText.gameObject.SetActive(!isFlipped);

        isFlipping = false;
    }

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
