using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenButtons : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.InitializeGame(numPlayers: 3);
        SceneManager.LoadScene("BumperCars 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        
    }
}
