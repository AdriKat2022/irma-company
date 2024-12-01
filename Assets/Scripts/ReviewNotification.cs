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
    // A star container to display the rating (made apart from the review notification to implement animations)
    //[SerializeField] private StarContainer starContainer;


    private void Start()
    {
        profilePicture.rectTransform.position = new Vector3(-750f, 350f, 0);
        numberOfStarsPicture.rectTransform.position = new Vector3(-300f, 250f, 0);
    }

    public void InitializeNotification(Sprite profilePicture, string username, int nbOfStars)
    {
        this.profilePicture.sprite = profilePicture;
        usernameText.text = username;
        numberOfStars = nbOfStars;
        getNumberOfStars();
    }

    public void MakeReview(string content)
    {
        contentText.text = content;
    }

    private void getNumberOfStars()
    {
        numberOfStarsPicture = Resources.Load("Assets/Resources/Images/Review/{$numberOfStars}etoile.png") as Image;
    }
}
