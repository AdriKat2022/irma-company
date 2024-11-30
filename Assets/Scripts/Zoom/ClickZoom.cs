using UnityEngine;

public class ClickZoom : MonoBehaviour
{
    [SerializeField] private GameObject baseImage;
    [SerializeField] private GameObject image;
    private bool isImageActive;
    

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
            baseImage.SetActive(true);
            isImageActive = false;
            ActivateObjectsWithTag("Eye");
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
                    baseImage.SetActive(false);
                    isImageActive = true;
                    DeactivateObjectsWithTag("Eye");
                }
            }
        }
    }
    void DeactivateObjectsWithTag(string tag)
    {
        // Find all objects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Loop through and deactivate each one
        foreach (GameObject obj in objectsWithTag)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;     
        }
    }

    void ActivateObjectsWithTag(string tag)
    {
        // Find all objects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Loop through and deactivate each one
        foreach (GameObject obj in objectsWithTag)
        {
            obj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
