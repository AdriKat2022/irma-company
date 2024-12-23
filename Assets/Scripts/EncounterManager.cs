using System.Collections;
using TMPro;
using UnityEngine;

public enum EncounterState
{
    Intro, // Introduction de l'encounter
    InDialogue, // Inspection possible en parall�le du dialogue
    Divination, // Phase de divination on peut choisir parmi trois cartes
    Outro // la carte a �t� choisie, on affiche le r�sultat
}

public class EncounterManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private CustomerData customerData;
    [SerializeField]
    private GameObject customer;

    [Header("Options")]
    [SerializeField]
    private float cardSpacing = 3f;

    [Header("Pacing")]
    [SerializeField]
    private float delayBeforeDialogue = 1.5f;
    [SerializeField]
    private float delayBeforeBackgroundDialogue = 1.5f;
    [SerializeField]
    private float nextQuestionDelay = 2f;
    [SerializeField]
    private float cardsFlipStepDelay = 0.35f;
    [SerializeField]
    private float delayBeforeReview = 2f;

    [Header("References")]
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private GameObject questionTextObject;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TarotCard tarotCardPrefab;
    [SerializeField]
    private GameObject confirmPopup;
    [SerializeField]
    private ReviewNotification notification;

    private int currentQuestionIndex = 0;

    private EncounterState currentState = EncounterState.Intro;
    private TarotCard[] tarotCards;
    private bool cardSelected = true;

    private string reviewContent;
    private int starCount;

    private void Start()
    {
        // TODO: should be called by the game manager
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameThemeMusic);
        tarotCards = new TarotCard[3];
        questionTextObject.SetActive(false);
    }

    public void StartEncounter()
    {
        StartEncounter(customerData, customer);
    }

    public void StartEncounter(CustomerData customerData, GameObject customer)
    {
        reviewContent = "";
        starCount = 0;
        currentQuestionIndex = 0;

        print("Encounter started with " + customerData.ToString());

        this.customerData = customerData;
        this.customer = customer;

        currentState = EncounterState.Intro;

        StartCoroutine(StartEncounterCoroutine());
    }

    public void RequestDivinationPhase(bool isConfirmed)
    {
        if (currentState != EncounterState.InDialogue) return;

        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.clickSound);
        confirmPopup.SetActive(true);

        // If the player confirms (means this was called by the confirmation button)
        if (isConfirmed)
        {
            dialogueManager.StopDialogue();
            AudioManager.Instance.TooglePauseVoice(false);
            confirmPopup.SetActive(false);
            ContinueDivinationPhase();
        }
    }

    public void ContinueDivinationPhase()
    {
        print("Divination phase continue");

        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.divinationBeginSound);

        currentState = EncounterState.Divination;

        var question = customerData.Questions[currentQuestionIndex];

        questionText.text = question.InitialSentence;
        questionTextObject.SetActive(true);
        print("Set text: " + question.InitialSentence);

        // Display the tarot cards
        for (int i = 0; i < question.AvailableCards.Length; i++)
        {
            // Instantiate the cards around the center of the screen (one card at the center, one on the left, one on the right)
            var card = tarotCards[i] == null ? Instantiate(tarotCardPrefab, new Vector3((i - 1) * cardSpacing, 0, 0), Quaternion.identity, transform) : tarotCards[i];
            tarotCards[i] = card;

            card.gameObject.SetActive(true);
            card.InitiateCard(question.AvailableCards[i], () => StartCoroutine(OnCardClicked(card)), faceDown: true);

            // Flip the card normally, if this is the last one, flip it with a callback
            if (i < question.AvailableCards.Length - 1) card.FlipCard(3);
            else card.FlipCard(3, () => {
                cardSelected = false;
                Debug.Log("Tarot cards are ready to be selected");
            });
        }
    }

    private IEnumerator OnCardClicked(TarotCard card)
    {
        if (cardSelected) yield break;

        questionTextObject.SetActive(false);
        cardSelected = true;
        reviewContent += card.CardData.reviewLine + "\n";
        starCount += card.CardData.CharacterScore;
        StartCoroutine(FlipAllCards());
        print("The card was clicked " + card.CardData.Content + " that gives " + card.CardData.CharacterScore);
    }

    private IEnumerator FlipAllCards()
    {
        for (int i = 0; i < tarotCards.Length; i++)
        {
            if (i < tarotCards.Length - 1)
            {
                tarotCards[i].FlipCard(1);
                yield return new WaitForSeconds(cardsFlipStepDelay);
            }
            else tarotCards[i].FlipCard(1, () => StartCoroutine(OnCardsFlipped()));
        }
    }

    private IEnumerator OnCardsFlipped()
    {
        
        currentQuestionIndex++;
        if (currentQuestionIndex < customerData.Questions.Length)
        {
            yield return new WaitForSeconds(nextQuestionDelay);
            ContinueDivinationPhase();
        }
        else
        {
            foreach (var card in tarotCards)
            {
                card.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.35f);
            }

            currentState = EncounterState.Outro;
            print("The encounter is over");
            customer.SetActive(false);
            yield return new WaitForSeconds(delayBeforeReview);
            // TODO: Display the review
            // Inform the game manager that the encounter is over

            notification.gameObject.SetActive(true);
            notification.InitializeNotification(customerData.ProfilePicture, customerData.Username, starCount/6, reviewContent);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            notification.gameObject.SetActive(false);
            gameManager.OnEndEncounter();
        }
    }

    private IEnumerator StartEncounterCoroutine()
    {
        customer.SetActive(true);
        yield return new WaitForSeconds(delayBeforeDialogue);
        dialogueManager.SetNewCustomer(customerData);
        dialogueManager.SetNewDialogue(customerData.IntroDialogue);
        dialogueManager.StartDialogue(() => StartCoroutine(OnIntroDialogueFinished()));
        print("Launched intro dialogue");
    }

    private IEnumerator OnIntroDialogueFinished()
    {
        print("The intro Dialogue finished");
        currentState = EncounterState.InDialogue;
        // TODO: Make the player able to inspect the customer
        yield return new WaitForSeconds(delayBeforeBackgroundDialogue);
        dialogueManager.SetNewDialogue(customerData.Dialogue);
        dialogueManager.StartDialogue(OnInDialogueFinished);
        print("Launched background dialogue");
    }

    private void OnInDialogueFinished()
    {
        // Nothing for now
        print("The background Dialogue finished");
    }
}
