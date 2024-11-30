using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad; 
    public GameObject SettingsWindow;
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
