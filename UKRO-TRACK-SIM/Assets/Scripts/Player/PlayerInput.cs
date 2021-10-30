using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CarOutputSample))]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    
    private PlayerInputManager playerInputManager;

    private bool playerDesireLightsState;
    private bool playerDesireSoundsState;

    private bool rhythmDesireLightsState;
    private bool rhythmDesireSoundsState;

    [SerializeField] private float rightActions;
    [SerializeField] private float wrongActions;

    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI wrongText;

    private CarOutputSample _carOutputSample;
    
    private void Awake()
    {
        Instance = this;
        
        playerInputManager = new PlayerInputManager();

        playerInputManager.PlayerMap.Lights.started += ctx => SetLightsState(true);
        playerInputManager.PlayerMap.Lights.canceled += ctx => SetLightsState(false);

        playerInputManager.PlayerMap.Sounds.started += ctx => SetSoundsState(true);
        playerInputManager.PlayerMap.Sounds.canceled += ctx => SetSoundsState(false);
        
        playerInputManager.Enable();
        _carOutputSample = GetComponent<CarOutputSample>();
    }

    private void SetLightsState(bool _val)
    {
        playerDesireLightsState = _val;
        _carOutputSample.SetLightsState(_val);
    }

    private void SetSoundsState(bool _val)
    {
        playerDesireSoundsState = _val;
        _carOutputSample.SetHornState(_val);
    }

    public void SetRhythmLightsState(bool _val)
    {
        rhythmDesireLightsState = _val;
    }

    public void SetRhythmSoundsState(bool _val)
    {
        rhythmDesireSoundsState = _val;
    }

    //separated comparing due to player multitask ability
    public void CompareStates()
    {
        if (playerDesireLightsState == rhythmDesireLightsState)
        {
            if(playerDesireLightsState) rightActions++;
        }
        else
        {
            if(!rhythmDesireSoundsState) wrongActions++;
        }

        if (playerDesireSoundsState == rhythmDesireSoundsState)
        {
            if(playerDesireSoundsState) rightActions++;
        }
        else
        {
            if(!playerDesireLightsState) wrongActions++;
        }

        rightText.text = rightActions.ToString();
        wrongText.text = wrongActions.ToString();
    }

    public void ShowScore()
    {
        float _sum = wrongActions + rightActions;
        
        Debug.Log(rightActions / _sum);
        wrongActions = 0;
        rightActions = 0;
    }
}
