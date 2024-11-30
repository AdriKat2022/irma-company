using UnityEngine;

public class MagnifyingGlass: MonoBehaviour
{
    [SerializeField] private float distanceFromCamera;
    private SpriteRenderer spriteRenderer;

    public Material magnifyMaterial;
    public float radius = 0.2f;
    public float power = 1.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceFromCamera; // Distance de la caméra
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Conversion de la position du monde vers UV (en supposant un espace de coordonnées normalisé)
        Vector2 uvPos = new Vector2(worldPos.x, worldPos.y);

        transform.position = worldPos;

        // Application des paramètres dans le matériau du shader
        magnifyMaterial.SetVector("_MagnifyCenter", new Vector4(uvPos.x, uvPos.y, 0, 0));
        magnifyMaterial.SetFloat("_MagnifyRadius", radius);
        magnifyMaterial.SetFloat("_MagnifyPower", power);

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
