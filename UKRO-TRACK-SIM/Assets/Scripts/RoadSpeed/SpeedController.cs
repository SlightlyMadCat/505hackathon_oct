using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

/**
 * Script control road moving (start/stop + speed)
 */
public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance;
    [Header("Set this value for change speed")]
    [SerializeField] private float speedCoef = 500f;
    [SerializeField] private float acceleration = 0.01f;
    [Header("Current speed debug")]
    [SerializeField] private float currentSpeedCoef = 0f;
    private float currentSpeed;
    private Coroutine speedCor;
    [SerializeField]  private int minSkipCar = 3; 
    private int minSkipCarOnStart = 3; 
    
    private void Awake()
    {
        Instance = this;
        StartMovement();
        minSkipCarOnStart = minSkipCar;
    }
    
    public void FixedUpdate()
    {
        currentSpeed = Time.deltaTime * currentSpeedCoef;
    }

    public void Update()
    {
        
    }

    // get current speed
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

   
    public void SetCurrentSpeedCoef(float _coef)
    {
        currentSpeedCoef = _coef;
    }

    [ContextMenu("stop")]
    public void StopMovement() // stop movement 
    {
        if(speedCor != null)
            StopCoroutine(speedCor);
        SetCurrentSpeedCoef(0);
        TimerUI.Instance.StartCoroutine(TimerUI.Instance.ShowCountDownWhenCarStop());
    }
    
    [ContextMenu("start")]
    public void StartMovement() // start movement
    {
        if(speedCor != null)
            StopCoroutine(speedCor);
        speedCor = StartCoroutine(MoveToThisSpeed(speedCoef));
    }


    // lerp start acceleration
    IEnumerator MoveToThisSpeed(float _targetVal)
    {
        while ( math.abs(currentSpeedCoef - _targetVal) > 0.5f)
        {
            float _newSpeedCoef = Mathf.Lerp(currentSpeedCoef, _targetVal, Time.deltaTime * acceleration);
            SetCurrentSpeedCoef(_newSpeedCoef);
            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Car")) return;
        
        // try stop car
        var _car = other.gameObject.GetComponent(typeof(CarStop)) as CarStop;
        if (_car != null && minSkipCar == 0) 
        {
            other.GetComponent<CarController>().SetInstance(true);
            StopMovement();
            minSkipCar = minSkipCarOnStart;
        }
        else
        {
            if (minSkipCar != 0)
                minSkipCar--;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Car")) return;
        
        // try disable car Instance
        var _car = other.gameObject.GetComponent(typeof(CarStop)) as CarStop;
        if (_car != null)
        {
            other.GetComponent<CarController>().SetInstance(false);
        }
    }
}
