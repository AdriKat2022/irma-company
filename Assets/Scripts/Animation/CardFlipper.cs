using System;
using System.Collections;
using UnityEngine;

public class CardFlipper : MonoBehaviour
{
    [SerializeField] private GameObject[] frontCardFeatures;
    [SerializeField] private Sprite faceSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField, Tooltip("Time it takes to flip 180°")] private float flipDuration = 0.5f;
    [SerializeField, Tooltip("Current state of the card")] private bool isFaceDown = true;

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

    // Setups the card with the given face sprite without any animation
    public void SetCurrentFaceSprite(Sprite faceSprite)
    {
        this.faceSprite = faceSprite;
        UpdateSprite();
    }
    
    public IEnumerator Flip(int repeat = 1, Action onFlipFinished = null)
    {
        Debug.Log($"Flipping {repeat} times", gameObject);

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
        Debug.Log("Flip Finished", gameObject);
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

        // Rotate from 0° to 90° (to invisible edge)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(0, 90, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, transform.rotation.eulerAngles.z);
            yield return null;
        }

        // Midpoint reached, no visible side (sprite change happens here)
        Debug.Log("MID POINT", gameObject);
        isFaceDown = !isFaceDown;
        UpdateSprite();
        elapsedTime = 0f;

        // Rotate back from 90° to 0° (to facing the player)
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float rotation = Mathf.Lerp(90, 0, elapsedTime / (flipDuration / 2));
            transform.rotation = Quaternion.Euler(0, rotation, transform.rotation.eulerAngles.z);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("Rotation Finished", gameObject);
    }

    // Used to show/hide the features of the card, called when the card is flipped
    private void ToogleFeatures(bool show)
    {
        foreach (GameObject feature in frontCardFeatures)
        {
            feature.SetActive(show);
        }
    }
}
