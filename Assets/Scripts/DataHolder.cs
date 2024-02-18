using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataHolder : ScriptableObject
{
    public int currentPaddleSpeed = 60;
    public int minPaddleSpeed = 50;
    public int maxPaddleSpeed = 120;

    [Space] public int dotsRemaining = 0;
    public int currentLevel = 1;
    public int stars = 0;

    [Space] public int minSpawnAngle = 30;
    public int maxSpawnAngle = 60;

    [Space] public GameState gameState = GameState.Paused;

    public void ResetLevel()
    {
        dotsRemaining = currentLevel;
        gameState = GameState.Paused;
    }

    public void ResetData()
    {
        currentLevel = 1;
        dotsRemaining = currentLevel;
        stars = 0;
        gameState = GameState.Paused;
    }

    public void IncreaseSpeed(int value)
    {
        currentPaddleSpeed = Mathf.Min(currentPaddleSpeed + value, maxPaddleSpeed);
    }

    public void DecreaseSpeed(int value)
    {
        currentPaddleSpeed = Mathf.Max(currentPaddleSpeed - value, minPaddleSpeed);
    }

    public void StopGame()
    {
        gameState = GameState.Paused;
    }

    public void StartGame()
    {
        gameState = GameState.Playing;
    }
}

public enum GameState
{
    Playing,
    Paused
}