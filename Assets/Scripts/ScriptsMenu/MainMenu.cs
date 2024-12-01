using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad; 
    public GameObject SettingsWindow;

    void Start()
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
