using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private float splashScreenDuration = 3f;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(splashScreenDuration);
        SceneManager.LoadScene(1);
    }
}
