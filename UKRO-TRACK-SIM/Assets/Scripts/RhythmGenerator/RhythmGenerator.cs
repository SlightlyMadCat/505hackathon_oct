using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private enum SignalType
        {
            light,
            sound
        }

        private SignalType signalType;
        [SerializeField] float duration;
        [SerializeField] float beforeSignalDelay;

        public SignalSample(float _duration, float _beforeSignalDelay, int _signalTypeId)
        {
            duration = _duration;
            beforeSignalDelay = _beforeSignalDelay;

            signalType = (SignalType)_signalTypeId;
        }

        public float GetDuration()
        {
            return duration;
        }

        public float GetBeforeSignalDelay()
        {
            return beforeSignalDelay;
        }

        public int GetSignalType()
        {
            //0-light, 1-sound
            return (int)signalType;
        }
    }

    [Serializable]
    public class RhythmSample
    {
        [SerializeField] private List<SignalSample> signalSamples = new List<SignalSample>();
        private List<SignalSample> copySignals = new List<SignalSample>();
        [SerializeField] private int chosenSignalTypeId;

        public RhythmSample(ComplexityLevelSample _complexity)
        {
            chosenSignalTypeId = Random.Range(0, _complexity.allowedSignals.Count);

            //new rhythm signals generation
            for (int i = 0; i < _complexity.RandomSignalsCount(); i++)
            {
                int _signalType = 0;

                switch (chosenSignalTypeId)
                {
                    case 0:
                        _signalType = 0;
                        break;
                    case 1:
                        _signalType = 1;
                        break;
                    case 2:
                        _signalType = Random.Range(0, 2);
                        break;
                }

                SignalSample _newSignal = new SignalSample(_complexity.RandomSignalDuration(),
                    _complexity.RandomBeforeSignalDelay(), _signalType);

                signalSamples.Add(_newSignal);
                copySignals.Add(_newSignal);
            }
        }

        public List<SignalSample> GetSignalSequence()
        {
            return signalSamples;
        }

        public float GetTotalSignalsTime()
        {
            float _totalTime = 0;

            foreach (var VARIABLE in signalSamples)
            {
                _totalTime += VARIABLE.GetDuration();
                _totalTime += VARIABLE.GetBeforeSignalDelay();
            }

            return _totalTime;
        }

        public void RestoreSignals()
        {
            signalSamples = copySignals;
        }
    }
    
    [Serializable]
    public class ComplexityLevelSample
    {
        [SerializeField] private string name;
        [SerializeField] private float requiredPlayerLevel;
        [SerializeField] private Vector2 signalsCountBounds;
        [SerializeField] private Vector2 signalDurationBounds;
        [SerializeField] private Vector2 signalDelayBounds;

        public enum AllowedSignals
        {
            light,
            sounds,
            both
        }

        public List<AllowedSignals> allowedSignals = new List<AllowedSignals>();

        public bool AllowedToUse(int _curPlayerLevel)
        {
            return _curPlayerLevel >= requiredPlayerLevel;
        }

        public int RandomSignalsCount()
        {
            return (int)Random.Range(signalsCountBounds.x, signalsCountBounds.y);
        }

        public float RandomSignalDuration()
        {
            return Random.Range(signalDurationBounds.x, signalDurationBounds.y);
        }

        public float RandomBeforeSignalDelay()
        {
            return Random.Range(signalDelayBounds.x, signalDelayBounds.y);
        }
    }

    [SerializeField] private List<ComplexityLevelSample> complexityLevelSamples = new List<ComplexityLevelSample>();
    public float globalTimeScaler = 2f;
    
    [ContextMenu("Generate")]
    public void GenerateNewSignal()
    {
        //get random complexity here:

        int _randomLevel = 0;
        bool _levelConfirmed = false;
        
        while (!_levelConfirmed)
        {
            _randomLevel = Random.Range(0, complexityLevelSamples.Count);
            if (complexityLevelSamples[_randomLevel].AllowedToUse(Player.Instance.GetCurrentPlayerLevel()))
                _levelConfirmed = true;
        }
        
        RhythmSample _newRhythm = new RhythmSample(complexityLevelSamples[_randomLevel]);
        PlayRhythm(_newRhythm);
    }
    
    private void PlayRhythm(RhythmSample _generatedRhytm)
    {
        CarController.Instance.PlayNewRhythm(_generatedRhytm, false);
    }
}
