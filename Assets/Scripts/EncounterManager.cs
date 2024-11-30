using System.Collections;
using UnityEngine;

public enum EncounterState
{
    Intro, // Introduction de l'encounter
    InDialogue, // Inspection possible en parallèle du dialogue
    Divination, // Phase de divination on peut choisir parmi trois cartes
    Outro // la carte a été choisie, on affiche le résultat
}

public class EncounterManager : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private float delayBeforeDialogue = 1.5f;
    [SerializeField]
    private CustomerData customerData;
    [SerializeField]
    private GameObject customer;

    private EncounterState currentState = EncounterState.Intro;

    public void StartEncounter()
    {
        StartEncounter(customerData, customer);
    }

    private void Start()
    {
        StartEncounter(customerData, customer);
    }

    private void StartEncounter(CustomerData customerData, GameObject customer)
    {
        this.customerData = customerData;
        this.customer = customer;

        currentState = EncounterState.Intro;
        customer.SetActive(true);

        StartCoroutine(StartEncounterCoroutine());
    }

    private IEnumerator StartEncounterCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeDialogue);
        dialogueManager.SetNewCustomer(customerData);
    }

    private void OnIntroDialogueFinished()
    {
        currentState = EncounterState.InDialogue;
    } 

    private void OnInDialogueFinished()
    {
        currentState = EncounterState.Divination;
    }

    private void OnDivinationFinished()
    {
        currentState = EncounterState.Outro;
    }
}
