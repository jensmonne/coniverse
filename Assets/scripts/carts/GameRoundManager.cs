using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameRoundManager : MonoBehaviour
{
    public List<Health> players;
    public static GameRoundManager Instance;
    [SerializeField] private GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

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
        List<Health> alivePlayers = players.Where(player => player.IsAlive).ToList();

        if (alivePlayers.Count == 1) // Only one player alive, they win
        {
            var survivingPlayer = alivePlayers[0];
        
            GameManager.Instance.AddScore(survivingPlayer.PlayerID, 1);
            Debug.Log($"Surviving Player: {survivingPlayer.gameObject.name} awarded 1 point.");
        }
        else if (alivePlayers.Count == 0) // No players alive, something went wrong
        {
            Debug.LogWarning("No surviving player found! Something went wrong.");
        }
        else 
        {
            // Still more than one player alive, no need to reset scene yet
            return;
        }

        // Round end logic
        if (GameManager.Instance.IsGameOver())
        {
            SceneManager.LoadScene("Titlescreen");
        }
        else
        {
            GameManager.Instance.NextRound();
            SceneManager.LoadScene("BumperCars");
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
            SceneManager.LoadScene("Titlescreen");
        }
        else
        {
            // sets the new round up
            GameManager.Instance.NextRound();
            SceneManager.LoadScene("BumperCars");
        }
    }
}
