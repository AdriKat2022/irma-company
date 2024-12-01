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
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.googleReviewMusic);
        GetNumberOfStars();
    }

    private void GetNumberOfStars()
    {
        print($"{numberOfStars}etoile.png");
        this.numberOfStarsPicture.sprite = Resources.Load<Sprite>($"Images/Review/{numberOfStars}etoile");
    }
}
