using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Percent for update level")] [SerializeField]
    private float minValueForNextLevel = 0.65f;

    
    [SerializeField] private GameObject scoreObj;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Color goodColor;
    [SerializeField] private Color badColor;
    [Space] 
    [SerializeField] private Image imageScore;
    [SerializeField] private Sprite goodSprite;
    [SerializeField] private Sprite badSprite;
    
    
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
        var result = rightActions / _sum;
        if (result > minValueForNextLevel)
            Player.Instance.UpdateLevel();
        else
            Player.Instance.MinusLive();

        
        wrongActions = 0;
        rightActions = 0;
        StartCoroutine(ShowResult(result));
        Debug.LogError("Show start");
    }

    IEnumerator ShowResult(float _result)
    {
        if (_result > minValueForNextLevel)
        {
            imageScore.sprite = goodSprite;
            scoreText.text = _result.ToString("0.00");
            scoreText.color = goodColor;
        }
        else
        {
            imageScore.sprite = badSprite;
            scoreText.text = _result.ToString("0.00");
            scoreText.color = badColor;
        }
        scoreObj.SetActive(true);
        yield return new WaitForSeconds(3);
        scoreObj.SetActive(false);
        SpeedController.Instance.StartMovement();
    }
}
