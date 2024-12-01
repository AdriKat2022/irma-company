using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DivinationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite normalDeck;
    [SerializeField]
    private Sprite hoverDeck;
    [SerializeField]
    private Image image;

    private bool isMouseHovering;
    public AudioSource audioSourceSurvoler;
    public AudioClip soundSurvoler;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSourceSurvoler.PlayOneShot(soundSurvoler);
        print("that works");
        image.sprite = hoverDeck;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalDeck;
    }
}
