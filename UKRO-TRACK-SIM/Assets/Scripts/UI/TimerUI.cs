using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    #region Singleton

    public static TimerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [SerializeField] private Image timerImg;
    private float timerScaleK;
    
    private void SetTimerImgState(bool _val)
    {
        timerImg.gameObject.SetActive(_val);
        timerImg.fillAmount = 1;
    }

    public void StartTimer(float _time)
    {
        SetTimerImgState(true);
        timerScaleK = 1f / _time;
    }
    
    private void FixedUpdate()
    {
        if (timerImg.gameObject.activeSelf)
        {
            //timer countdown
            timerImg.fillAmount -= (timerScaleK / 1.05f * Time.fixedDeltaTime);
            
            if(timerImg.fillAmount <= 0f) SetTimerImgState(false);
        }
    }
}
