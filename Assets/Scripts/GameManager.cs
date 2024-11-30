using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EncounterManager encounterManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        encounterManager.StartEncounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEndEncounter()
    {
        encounterManager.StartEncounter();
    }
}
