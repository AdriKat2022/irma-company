using UnityEngine;

public class ZoomShader : MonoBehaviour
{
    [SerializeField] private Material material;

    void Update()
    {
        Vector2 screenPixels = Camera.main.WorldToScreenPoint(transform.position);
        screenPixels = new Vector2(screenPixels.x / Screen.width, screenPixels.y / Screen.height);

        material.SetVector("_ObjectScreenPosition", screenPixels);
    }
}
