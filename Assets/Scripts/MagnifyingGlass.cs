using UnityEngine;

public class MagnifyingGlass: MonoBehaviour
{
    [SerializeField] private float distanceFromCamera;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = distanceFromCamera;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = worldPosition;

        if(Input.GetMouseButtonDown(1)) 
        { 
            Deactivate();
        }
    }

    void Deactivate()
    {
        Cursor.visible = true;
        spriteRenderer.enabled = false;
    }

    public void Activate()
    {
        Cursor.visible = false;
        spriteRenderer.enabled = true;
    }
}
