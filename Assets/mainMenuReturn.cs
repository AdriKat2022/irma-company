using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuReturn : MonoBehaviour
{
    public string LevelToLoad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
