using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class TimerUI : MonoBehaviour
{
    #region Singleton

    public static TimerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [SerializeField] private Slider timerImg;
    private float timerScaleK;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tilesParent;
    private List<Transform> spawnedTiles = new List<Transform>();

    [Space] [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TextMeshProUGUI countDownText;
    
    private void SetTimerImgState(bool _val)
    {
        timerImg.gameObject.SetActive(_val);
        timerImg.value = 0;

        if (!_val)
        {
            HideTiles();
            PlayerInput.Instance.ShowScore();
        }
    }

    public void StartTimer(RhythmGenerator.RhythmSample _rhythm)
    {
        SetTimerImgState(true);
        Debug.Log(_rhythm.GetTotalSignalsTime());
        timerScaleK = 1f / _rhythm.GetTotalSignalsTime();
        SpawnTiles(_rhythm);
    }

    private void HideTiles()
    {
        foreach (var VARIABLE in spawnedTiles)
        {
            Destroy(VARIABLE.gameObject);
        }
        spawnedTiles.Clear();
    }

    private void SpawnTiles(RhythmGenerator.RhythmSample _rhythm)
    {
        float _totalWidth = 1280f;  //ref screen width
        float _scalerK = _totalWidth / _rhythm.GetTotalSignalsTime();
        
        foreach (var VARIABLE in _rhythm.GetSignalSequence())
        {
            TileSample _newTile = Instantiate(tilePrefab).GetComponent<TileSample>();
            _newTile.Init(tilesParent);
            
            Vector2 _sizeDelta = _newTile.parentRect.sizeDelta;

            if (_rhythm.GetSignalSequence().IndexOf(VARIABLE) != _rhythm.GetSignalSequence().Count - 1)
            {
                _newTile.SetParentSizeScale(
                    new Vector2(_scalerK * (VARIABLE.GetDuration() + VARIABLE.GetNextSignalDelay()), _sizeDelta.y));
            }
            else
            {
                _newTile.SetParentSizeScale(new Vector2(_scalerK * VARIABLE.GetDuration(), _sizeDelta.y));
            }
            _newTile.SetSignalImgSizeScale(new Vector2(_scalerK * VARIABLE.GetDuration(), _sizeDelta.y));
            
            spawnedTiles.Add(_newTile.transform);
        }
    }
    
    private void FixedUpdate()
    {
        if (timerImg.gameObject.activeSelf)
        {
            //timer countdown
            timerImg.value += (timerScaleK / 1.05f * Time.fixedDeltaTime);
            PlayerInput.Instance.CompareStates();
            
            if(timerImg.value >= 1f) SetTimerImgState(false);
        }
    }

    public void StartCountDown(RhythmGenerator.RhythmSample _rhythm)
    {
        StartCoroutine(CountDownCor(_rhythm));
    }

    IEnumerator CountDownCor(RhythmGenerator.RhythmSample _rhythm)
    {
        countdownPanel.SetActive(true);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownPanel.gameObject.SetActive(false);
        StopCountDown(_rhythm);
    }

    private void StopCountDown(RhythmGenerator.RhythmSample _rhythm)
    {
        StartTimer(_rhythm);
        CarController.Instance.PlayNewRhythm(_rhythm, true);
    }
}
