using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenButtons : MonoBehaviour
{
    private int players;

    private void Start()
    {
        players = CountConnectedGamepads();
        Debug.Log($"Number of connected gamepads: {players}");
    }

    public void StartGame()
    {
        if (players == 0)
        {
            Debug.LogWarning("No gamepads connected. Defaulting to 1 player.");
            players = 1;
        }

        GameManager.Instance.InitializeGame(numPlayers: players);
        SceneManager.LoadScene("BumperCars");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        
    }

    private int CountConnectedGamepads()
    {
        string[] gamepadNames = Input.GetJoystickNames();
        int count = 0;

        foreach (string gamepadName in gamepadNames)
        {
            if (!string.IsNullOrEmpty(gamepadName))
            {
                count++;
            }
        }

        return count;
    }
}