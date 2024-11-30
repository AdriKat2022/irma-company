using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private bool isInputLocked = false; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isInputLocked)
        {
            isInputLocked = true; 
            Debug.Log("Escape key pressed");

            if (gameIsPaused)
            {
                Debug.Log("Game is paused, resuming...");
                Resume();
            }
            else
            {
                Debug.Log("Game is not paused, pausing...");
                Paused();
            }

            
            Invoke("UnlockInput", 0.2f); 
        }
    }

    private void UnlockInput()
    {
        isInputLocked = false;
    }

    public void Paused()
    {
        Debug.Log("Entering paused state");
        pauseMenuUI.SetActive(true); // Activer notre menu pause et l'afficher
        Time.timeScale = 0; // "Arrêter" le temps
        gameIsPaused = true; // Changer le statut du jeu
    }

    public void Resume()
    {
        Debug.Log("Exiting paused state");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
