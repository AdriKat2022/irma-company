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
    [SerializeField]
    private float delayBeforeDialogue = 1.5f;
    [SerializeField]
    private float delayBeforeBackgroundDialogue = 1.5f;
    [SerializeField]
    private float delayBeforeReveal = 2f;
    [SerializeField]
    private float delayBeforeReview = 2f;

    [Header("References")]
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private TextMeshProUGUI sentenceText;
    [SerializeField]
    private TarotCard tarotCardPrefab;
    [SerializeField]
    private GameObject confirmPopup;

    private int currentQuestionIndex = 0;

    private EncounterState currentState = EncounterState.Intro;
    private TarotCard[] tarotCards;
    private bool cardSelected = true;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameThemeMusic);
        tarotCards = new TarotCard[3];
        StartEncounter(customerData, customer);
    }


    public void StartEncounter()
    {
        StartEncounter(customerData, customer);
    }


    public void StartEncounter(CustomerData customerData, GameObject customer)
    {
        print("Encounter started with " + customerData.ToString());

        this.customerData = customerData;
        this.customer = customer;

        currentState = EncounterState.Intro;

        StartCoroutine(StartEncounterCoroutine());
    }

    public void RequestDivinationPhase(bool isConfirmed)
    {
        if (currentState != EncounterState.InDialogue) return;

        confirmPopup.SetActive(true);

        if (isConfirmed)
        {
            confirmPopup.SetActive(false);
            
            ContinueDivinationPhase();
        }
    }

    public void ContinueDivinationPhase()
    {
        print("Divination phase continue");

        AudioManager.Instance.TooglePauseMusic(false);
        AudioManager.Instance.TooglePauseVoice(false);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.divinationBeginSound);

        currentState = EncounterState.Divination;

        var question = customerData.Questions[currentQuestionIndex];

        // TODO: Display the sentence to complete

        // Display the tarot cards
        for (int i = 0; i < question.AvailableCards.Length; i++)
        {
            // Instantiate the cards around the center of the screen (one card at the center, one on the left, one on the right)
            var card = tarotCards[i] == null ? Instantiate(tarotCardPrefab, new Vector3((i - 1) * cardSpacing, 0, 0), Quaternion.identity, transform) : tarotCards[i];

            card.InitiateCard(question.AvailableCards[i], () => StartCoroutine(OnCardClicked(card)), faceDown: true);
            card.gameObject.SetActive(true);

            if (i == question.AvailableCards.Length - 1) card.FlipCard(3, () => cardSelected = false);
            else card.FlipCard(3);
            tarotCards[i] = card;
        }
    }

    private IEnumerator OnCardClicked(TarotCard card)
    {
        if (cardSelected) yield break;

        cardSelected = true;

        StartCoroutine(FlipAllCards());
        print("The card was clicked " + card.CardData.Content + " that gives " + card.CardData.CharacterScore);
    }

    private IEnumerator FlipAllCards()
    {
        foreach (var card in tarotCards)
        {
            card.FlipCard();
            yield return new WaitForSeconds(0.35f);
        }

        yield return new WaitForSeconds(1f);

        //foreach (var card in tarotCards)
        //{
        //    card.gameObject.SetActive(false);
        //    yield return new WaitForSeconds(0.35f);
        //}

        currentQuestionIndex++;
        if (currentQuestionIndex < customerData.Questions.Length)
        {
            ContinueDivinationPhase();
        }
        else
        {
            currentState = EncounterState.Outro;
            print("The encounter is over");
            yield return new WaitForSeconds(delayBeforeReveal);
            customer.SetActive(false);
            yield return new WaitForSeconds(delayBeforeReview);
            // TODO: Display the review
            // Inform the game manager that the encounter is over
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
