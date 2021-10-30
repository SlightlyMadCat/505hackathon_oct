using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Player logic container
 */

public class Player : MonoBehaviour
{
    #region Singleton

    public static Player Instance;
    
    private void Awake()
    {
        Instance = this;
        SetCurrentPlayerLevel(GetCurrentPlayerLevel());
        SetCurrentPlayerLives(playerLives);
        startPlayerLives = playerLives;
    }

    #endregion

    [SerializeField] private int playerLevel;
    [SerializeField] private TextMeshProUGUI levelText;
    [Space]
    [SerializeField] private int playerLives;
    [SerializeField] private TextMeshProUGUI livesText;
    private int startPlayerLives;
    public int GetCurrentPlayerLevel()
    {
        return playerLevel;
    }

    private void SetCurrentPlayerLevel(int level)
    {
        playerLevel = level;
        UpdateLevelCanvas(GetCurrentPlayerLevel());
    }
    
    public void SetCurrentPlayerLives(int lives)
    {
        playerLives = lives;
        UpdateLivesCanvas(playerLives);
    }

    public void UpdateLevel()
    {
        var newLvl = GetCurrentPlayerLevel() + 1;
        SetCurrentPlayerLevel(newLvl);
    }

    public void MinusLive()
    {
        playerLives--;
        if (playerLives < 0)
            ResetGame();
        else
            SetCurrentPlayerLives(playerLives);
    }
    
    // reset game
    private void ResetGame()
    {
        SetCurrentPlayerLevel(0);
        SetCurrentPlayerLives(startPlayerLives);
    }

    private void UpdateLevelCanvas(int _lvl)
    {
        levelText.text = _lvl.ToString();
    }
    
    private void UpdateLivesCanvas(int _lives)
    {
        livesText.text = _lives.ToString();
    }
    
    
}
