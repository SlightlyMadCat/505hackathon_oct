using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region Singleton

    public static CarController Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [SerializeField] private Light[] lights;
    [SerializeField] private AudioSource horn;
    
    public void SetLightsState(bool _val)
    {
        foreach (var VARIABLE in lights)
        {
            VARIABLE.enabled = _val;
        }
    }

    public void SetHornState(bool _val)
    {
        if (_val)
        {
            horn.Play();
        }
        else
        {
            horn.Stop();
        }
    }
}
