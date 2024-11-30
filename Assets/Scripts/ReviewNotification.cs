using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewNotification : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float reviewSpeed = 1f;

    [Header("References")]
    [SerializeField] private Image profilePicture;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI contentText;
    // A star container to display the rating (made apart from the review notification to implement animations)
    //[SerializeField] private StarContainer starContainer;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void InitializeNotification(Sprite profilePicture, string username)
    {
        this.profilePicture.sprite = profilePicture;
        usernameText.text = username;
    }

    public void MakeReview(string content, int starCount)
    {
        contentText.text = content;
    }

    private IEnumerator AnimateReview()
    {
        // Animate in the coroutine or make an animator for the review notification
        yield return null;
    }
}
