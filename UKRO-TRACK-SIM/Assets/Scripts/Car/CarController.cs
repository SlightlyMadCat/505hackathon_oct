using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Coroutine playedRhythm;

    private void SetLightsState(bool _val)
    {
        foreach (var VARIABLE in lights)
        {
            VARIABLE.enabled = _val;
        }
    }

    private void SetHornState(bool _val)
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

    public void PlayNewRhythm(RhythmGenerator.RhythmSample _newRhythm)
    {
        if(playedRhythm != null) StopCoroutine(playedRhythm);
        playedRhythm = StartCoroutine(DisplaySignal(_newRhythm.GetSignalSequence(), 0));
    }

    private IEnumerator DisplaySignal(List<RhythmGenerator.SignalSample> _signals, float _displayDelay)
    {
        float _newDelay = _signals[0].GetNextSignalDelay();
        //delay before new signal start
        yield return new WaitForSeconds(_displayDelay);
        
        //signal is shown
        if (_signals[0].GetSignalType() == 0)
        {
            SetLightsState(true);
            PlayerInput.Instance.SetRhythmLightsState(true);
        }
        else
        {
            SetHornState(true);
            PlayerInput.Instance.SetRhythmSoundsState(true);
        }

        yield return new WaitForSeconds(_signals[0].GetDuration());

        //signal is stopped
        SetHornState(false);
        SetLightsState(false);
        
        PlayerInput.Instance.SetRhythmLightsState(false);
        PlayerInput.Instance.SetRhythmSoundsState(false);
        
        _signals.RemoveAt(0);
        
        if(_signals.Count > 0)
            playedRhythm = StartCoroutine(DisplaySignal(_signals, _newDelay));
        else
            Debug.Log("rhythm ended");
    }
}
