using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameRoundManager : MonoBehaviour
{
    public List<Health> players;
    public static GameRoundManager Instance;

    private void Start()
    {

        players = new List<Health>(FindObjectsOfType<Health>());


        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerID = i;  
        }

        Debug.Log($"Round {GameManager.Instance.currentRound} of {GameManager.Instance.totalRounds}");
    }

    public void CheckForRoundWinner()
{
    Health survivingPlayer = null;
    
    foreach (var player in players)
    {
        if (player.IsAlive)
        {
            survivingPlayer = player;
            break; 
        }
    }

    
    if (survivingPlayer != null)
    {
        // Award points to the player(s) who died
        GameManager.Instance.AddScore(survivingPlayer.PlayerID, 1);
        Debug.Log($"Surviving Player: {survivingPlayer.gameObject.name} awarded 1 point.");
    }
    else
    {
        Debug.LogWarning("No surviving player found! Something went wrong.");
    }

    if (GameManager.Instance.IsGameOver())
    {
        Debug.Log("Game Over");
    }
    else
    {
        // Proceed to the next round
        GameManager.Instance.NextRound();
        SceneManager.LoadScene("BumperCasr");
    }
}


    private void PlayerWins(Health winner)
    {
        int winnerIndex = winner.PlayerID; 

        if (winnerIndex < 0 || winnerIndex >= GameManager.Instance.playerScores.Count)
        {
            Debug.LogError("Winner's index is invalid!");
            return;
        }

        Debug.Log($"{winner.gameObject.name} wins the round!");

        // Assign the score immediately
        GameManager.Instance.AddScore(winnerIndex, 1);

        Debug.Log($"Player {winnerIndex} score updated: {GameManager.Instance.playerScores[winnerIndex]}");

        // Proceed to the next round after a delay so the animations can play
        StartCoroutine(GoToNextRoundAfterDelay(3f));
    }

    private IEnumerator GoToNextRoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (GameManager.Instance.IsGameOver())
        {
            Debug.Log("Game Over");
        }
        else
        {
            // sets the new round up
            GameManager.Instance.NextRound();
            SceneManager.LoadScene("BumberCars");
        }
    }
}
