using UnityEngine;

public class GrowthOnHover : MonoBehaviour
{
    [SerializeField]
    private float growthFactor = 1.2f; // How much the object grows when hovered
    [SerializeField]
    private float growthSpeed = 1f;    // Speed of the growth

    private Vector3 originalScale;
    private bool isHovered = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isHovered)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * growthFactor, Time.deltaTime * growthSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * growthSpeed);
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }
}
