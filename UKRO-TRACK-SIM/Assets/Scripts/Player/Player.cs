using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    #endregion

    [SerializeField] private int playerLevel;
}
