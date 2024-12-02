using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EncounterManager encounterManager;

    [SerializeField] private CustomerData[] customerDataList;
    [SerializeField] private GameObject[] customerList;

    private int currentCaracterCount;
    private int maxCaracterCount;

    private void Start()
    {
        maxCaracterCount = customerDataList.Length;
        encounterManager.StartEncounter(customerDataList[0], customerList[0]);
        currentCaracterCount++;
    }

    // Called by the EncounterManager when the encounter ends
    public void OnEndEncounter()
    {
        if (currentCaracterCount < maxCaracterCount)
        {
            encounterManager.StartEncounter(customerDataList[currentCaracterCount], customerList[currentCaracterCount]);
            currentCaracterCount++;
        }   
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(3);
    }
}
