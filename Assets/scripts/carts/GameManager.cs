using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalRounds;  // Total number of rounds
    public int currentRound; // Tracks the current round
    public int playerCount;  // Number of players
    public List<int> playerScores; // List of player scores

    // makes sure that there's always a GameManager in every scene
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    

    // sets the counts for the game
    public void InitializeGame(int numPlayers)
    {
        playerCount = numPlayers; 
        totalRounds = 3; 
        currentRound = 1; 

        // Initialize player scores
        playerScores = new List<int>(new int[playerCount]);
        
    }
    
    // Add's the deaths to the right player
    public void AddScore(int winnerIndex, int score)
    {
        if (winnerIndex < 0 || winnerIndex >= playerScores.Count) 
        {
            Debug.LogError($"Invalid winner index: {winnerIndex}");
            return;
        }

        playerScores[winnerIndex] += score; 
    }

    public bool IsGameOver()
    {
        return currentRound >= totalRounds; 
    }

    public void NextRound()
    {
        currentRound++; 
    }
}