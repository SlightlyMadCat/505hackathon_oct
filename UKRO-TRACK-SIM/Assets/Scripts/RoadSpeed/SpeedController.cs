using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
    private void Awake()
    {
        Instance = this;
        StartMovement(); 
    }
    
    public void FixedUpdate()
    {
        currentSpeed = Time.deltaTime * currentSpeedCoef;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A))
            StartMovement();
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
        Debug.LogError("STOP ----------");
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
        // generate new road
        if (other.gameObject.CompareTag("Car"))
        {
            var _car = other.gameObject.GetComponent(typeof(CarStop)) as CarStop;
            if (_car != null)
                StopMovement();
        }
    }

 
    
}
