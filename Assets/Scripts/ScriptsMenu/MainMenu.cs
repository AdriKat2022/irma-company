using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string LevelToLoad; 
    [SerializeField]
    private GameObject SettingsWindow;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void SettingsButton()
    {
        SettingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        SettingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}
