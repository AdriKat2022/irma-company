using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;

    private bool isInputLocked = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInputLocked)
        {
            isInputLocked = true; 
            Debug.Log("Escape key pressed");
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.pauseClickSound);
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
            Invoke(nameof(UnlockInput), 0.2f);
        }
    }

    private void UnlockInput()
    {
        isInputLocked = false;
    }

    public void Paused()
    {
        AudioManager.Instance.TooglePauseMusic(false);
        AudioManager.Instance.TooglePauseVoice(false);
        Debug.Log("Entering paused state");
        pauseMenuUI.SetActive(true); // Activer notre menu pause et l'afficher
        Time.timeScale = 0; // "Arrêter" le temps
        gameIsPaused = true; // Changer le statut du jeu
    }

    public void Resume()
    {
        AudioManager.Instance.TooglePauseMusic(true);
        AudioManager.Instance.TooglePauseVoice(true);
        Debug.Log("Exiting paused state");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
