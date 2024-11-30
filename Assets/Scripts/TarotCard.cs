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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipCard()
    {
        if (!isFlipping) StartCoroutine(Flip());
    }

    public void InitiateCard(CardSlot cardData, Action callBack = null)
    {
        this.cardData = cardData;
        onClick = callBack;
    }

    private IEnumerator Flip()
    {
        isFlipping = true;
        float elapsedTime = 0f;

        // Rotate to 90 degrees (invisible edge)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(0, 90, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }

        // Swap sprite at the halfway point
        isFlipped = !isFlipped;
        spriteRenderer.sprite = isFlipped ? frontSprite : backSprite;

        elapsedTime = 0f;

        // Rotate back to 0 degrees (visible face)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(90, 180, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0); // Ensure exact alignment
        isFlipping = false;
    }

    private void OnMouseDown()
    {

    }
}
