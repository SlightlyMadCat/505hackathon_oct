using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CarOutputSample))]
public class CarController : MonoBehaviour
{
    #region Singleton

    public static CarController Instance;

    private void Awake()
    {
        Instance = this;
        _carOutputSample = GetComponent<CarOutputSample>();
    }

    #endregion

    private CarOutputSample _carOutputSample;
    private Coroutine playedRhythm;
    
    //set to false if we want just display car lights without player actions check
    private RhythmGenerator.RhythmSample _rhythmCopy;
    
    public void PlayNewRhythm(RhythmGenerator.RhythmSample _newRhythm, bool _checkerMode)
    {
        if(playedRhythm != null) StopCoroutine(playedRhythm);
        _rhythmCopy = _newRhythm;
        
        playedRhythm = StartCoroutine(DisplaySignal(_newRhythm.GetSignalSequence(), 0, _checkerMode));
    }

    private IEnumerator DisplaySignal(List<RhythmGenerator.SignalSample> _signals, float _displayDelay, bool _checkerMode)
    {
        float _newDelay = _signals[0].GetNextSignalDelay();
        //delay before new signal start
        yield return new WaitForSeconds(_displayDelay);
        
        //signal is shown
        if (_signals[0].GetSignalType() == 0)
        {
            if(!_checkerMode)
                _carOutputSample.SetLightsState(true);
            else
                PlayerInput.Instance.SetRhythmLightsState(true);
        }
        else
        {
            if(!_checkerMode)
                _carOutputSample.SetHornState(true);
            else
                PlayerInput.Instance.SetRhythmSoundsState(true);
        }

        yield return new WaitForSeconds(_signals[0].GetDuration());

        //signal is stopped
        if (!_checkerMode)
        {
            if (_signals[0].GetSignalType() == 0) _carOutputSample.SetLightsState(false);
            else _carOutputSample.SetHornState(false);
        }
        else
        {
            PlayerInput.Instance.SetRhythmLightsState(false);
            PlayerInput.Instance.SetRhythmSoundsState(false);
        }

        _signals.RemoveAt(0);
        
        if(_signals.Count > 0)
            playedRhythm = StartCoroutine(DisplaySignal(_signals, _newDelay, _checkerMode));
        else
        {
            if (!_checkerMode)
            {
                //rhythm is shown to the player
                Debug.Log("rhythm ended");
                //start countdown before player's check
                _rhythmCopy.RestoreSignals();
                TimerUI.Instance.StartCountDown(_rhythmCopy);
            }
            else
            {
                //player actions check ended
            }
        }
    }
}
