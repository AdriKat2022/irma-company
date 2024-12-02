using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuReturn : MonoBehaviour
{
    public string LevelToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
