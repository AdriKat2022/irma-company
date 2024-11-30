using UnityEngine;

public class CardIdleAnimation : MonoBehaviour
{
    [SerializeField]
    private bool startOnAwake = true;   // Whether the animation should start when the object is created

    [SerializeField]
    private float floatAmplitude = 0.2f; // How much the card floats up and down
    [SerializeField]
    private float floatSpeed = 1f;      // Speed of the floating
    [SerializeField]
    private float rotationAmplitude = 5f; // Rotation range in degrees
    [SerializeField]
    private float rotationSpeed = 1f;     // Speed of the rotation

    private Vector3 startPos;
    private float rotationTime;
    private float translationTime;
    private bool isAnimating = false;

    public void ToogleAnimation(bool anim) => isAnimating = anim;

    private void Start()
    {
        startPos = transform.position;
        rotationTime = Random.Range(0f, 2f * Mathf.PI); // Offset to avoid all cards moving synchronously
        translationTime = Random.Range(0f, 2f * Mathf.PI); // Offset to avoid all cards moving synchronously
        isAnimating = startOnAwake;
    }

    private void Update()
    {
        if (!isAnimating) return;

        // Floating effect
        float floatOffset = Mathf.Sin(translationTime * floatSpeed) * floatAmplitude;
        transform.position = startPos + new Vector3(0, floatOffset, 0);

        // Rotation effect
        float rotationOffset = Mathf.Sin(rotationTime * rotationSpeed) * rotationAmplitude;
        transform.rotation = Quaternion.Euler(0, 0, rotationOffset);

        // Update time
        rotationTime += Time.deltaTime;
        translationTime += Time.deltaTime;
    }
}
