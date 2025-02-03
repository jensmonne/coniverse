using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        
    }
}
