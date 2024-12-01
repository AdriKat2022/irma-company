using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewNotification : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image profilePicture;
    [SerializeField] private Image numberOfStarsPicture;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI contentText;

    private int numberOfStars;

    public void InitializeNotification(Sprite profilePicture, string username, int nbOfStars, string content)
    {
        this.profilePicture.sprite = profilePicture;
        usernameText.text = username;
        numberOfStars = nbOfStars;
        contentText.text = content;
        GetNumberOfStars();
    }

    private void GetNumberOfStars()
    {
        numberOfStarsPicture = Resources.Load("Assets/Resources/Images/Review/{$numberOfStars}etoile.png") as Image;
    }
}
