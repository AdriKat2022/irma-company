using System;
using System.Collections;
using UnityEngine;

public class CardFlipper : MonoBehaviour
{
    [SerializeField] private GameObject[] frontCardFeatures; // Features of the card's face
    [SerializeField] private Sprite faceSprite; // Sprite for the face of the card
    [SerializeField] private Sprite backSprite; // Sprite for the back of the card
    [SerializeField] private float flipDuration = 0.5f; // Time it takes to flip 180°
    [SerializeField] private bool isFaceDown = true; // Current state of the card

    private bool isFlipping = false; // Flag to prevent multiple flips
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite(); // Ensure the correct sprite is shown at start
    }


    private void UpdateSprite()
    {
        spriteRenderer.sprite = isFaceDown ? backSprite : faceSprite;
        ToogleFeatures(!isFaceDown);
    }

    public void SetCurrentFaceSprite(Sprite faceSprite)
    {
        this.faceSprite = faceSprite;
        UpdateSprite();
    }
    
    public IEnumerator Flip(int repeat = 1, Action onFlipFinished = null)
    {
        Debug.Log($"FLipping {repeat} times", gameObject);

        if (isFlipping)
        {
            yield break;
        }
        isFlipping = true;

        for (int i = 0; i < repeat; i++)
        {
            yield return StartCoroutine(FlipOnce());
        }

        isFlipping = false;
        onFlipFinished?.Invoke();
    }

    public void SetFaceDown(bool faceDown)
    {
        isFaceDown = faceDown;
        UpdateSprite();
    }

    private IEnumerator FlipOnce()
    {
        float elapsedTime = 0f;

        // Rotate to 90° (invisible edge)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(0, 90, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, transform.rotation.eulerAngles.z);
            yield return null;
        }

        // Midpoint reached, no visible side (sprite change happens here)
        isFaceDown = !isFaceDown; // Toggle the card state
        UpdateSprite();
        elapsedTime = 0f;
        print("MID POINT");

        // Rotate back to 180° (visible side)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(90, 0, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, transform.rotation.eulerAngles.z);
            yield return null;
        }

        // Ensure the final rotation is correct
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void ToogleFeatures(bool show)
    {
        foreach (GameObject feature in frontCardFeatures)
        {
            feature.SetActive(show);
        }
    }
}
