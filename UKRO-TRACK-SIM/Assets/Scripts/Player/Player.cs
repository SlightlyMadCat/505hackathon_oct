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
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        Instance = this;
        SetCurrentPlayerLevel(GetCurrentPlayerLevel());
    }

    #endregion

    [SerializeField] private int playerLevel;

    
    public int GetCurrentPlayerLevel()
    {
        return playerLevel;
    }

    public void SetCurrentPlayerLevel(int level)
    {
        playerLevel = level;
        UpdateLevelCanvas(GetCurrentPlayerLevel());
    }

    public void UpdateLevel()
    {
        var newLvl = GetCurrentPlayerLevel() + 1;
        SetCurrentPlayerLevel(newLvl);
    }

    // reset game
    public void ResetGame()
    {
        SetCurrentPlayerLevel(0);
    }

    private void UpdateLevelCanvas(int _lvl)
    {
        levelText.text = _lvl.ToString();
    }
}
