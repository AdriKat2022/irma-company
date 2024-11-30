using UnityEngine;

public class ClickZoom : MonoBehaviour
{
    [SerializeField] private GameObject image;
    private bool isImageActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isImageActive) 
        {
            HandleClick();
        }
        else if(Input.GetMouseButtonDown(0) && isImageActive)
        {
            image.SetActive(false);
            isImageActive = false;
        }
    }

    private void HandleClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            // Check if the object clicked is this one
            if (hit.collider.gameObject == gameObject)
            {
                // Activate the other object
                if (image != null)
                {
                    image.SetActive(true);
                    isImageActive = true;
                }
            }
        }
    }
}
