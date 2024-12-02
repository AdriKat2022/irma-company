using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuReturnTemp : MonoBehaviour
{
    public string LevelToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
