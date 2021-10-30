using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main rhythm generator
 */

public class RhythmGenerator : MonoBehaviour
{
    #region Singletom

    public static RhythmGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [Serializable]
    public class SignalSample
    {
        public enum SignalType
        {
            light,
            sound
        }

        public SignalType signalType;
        public float duration;
        public float nextSignalDelay;
    }

    [Serializable]
    public class RhythmSample
    {
        [SerializeField] private List<SignalSample> signalSamples = new List<SignalSample>();
    }
    
    [Serializable]
    public class ComplexityLevelSample
    {
        [SerializeField] private string name;
        [SerializeField] private float requiredPlayerLevel;
        [SerializeField] private Vector2 signalDurationBounds;
        [SerializeField] private Vector2 signalDelayBounds;
        private enum AllowedSignals
        {
            light,
            sounds,
            both
        }

        [SerializeField] private List<AllowedSignals> allowedSignals = new List<AllowedSignals>();
    }

    [SerializeField] private List<ComplexityLevelSample> complexityLevelSamples = new List<ComplexityLevelSample>();

    
    public void GenerateNewSignal()
    {
        RhythmSample _newRhythm = new RhythmSample();
    }

    public void PlayRhythm(RhythmSample _generatedRhytm)
    {
        
    }
}
