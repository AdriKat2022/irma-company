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

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.cardHoverSound);
        print("that works");
        image.sprite = hoverDeck;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalDeck;
    }
}
