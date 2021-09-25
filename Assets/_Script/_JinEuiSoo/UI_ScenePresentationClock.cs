using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScenePresentationClock : MonoBehaviour
{
    [SerializeField] GameObject _clockArrowGo;
    [SerializeField] float _clockMoveTime;
    [SerializeField] float _innerClockMoveTime;
    [SerializeField] bool _clockTickTockGoing;

    [SerializeField] GameObject _timerGo;
    [SerializeField] float _innerTimerTime;
    [SerializeField] bool _timerTickTockGoing;


    private void Update()
    {

        if(_clockTickTockGoing)
        {
            ClockTimeTickTock();
        }

        if(_timerTickTockGoing)
        {
            TimerTimeTickTock();
        }    
    }

    #region Clock

    public void SetClockTimeAndStart(float time)
    {
        _clockMoveTime = time;
        _innerClockMoveTime = time;
        _clockTickTockGoing = true;
    }

    public void SetClockStopAndInitialize()
    {
        _clockMoveTime = 0f;
        _clockTickTockGoing = false;

        UpdateClockArrowRotation();
    }

    void ClockTimeTickTock()
    {
        _innerClockMoveTime -= Time.deltaTime;
        if (_innerClockMoveTime <= 0f)
        {
            _clockTickTockGoing = false;
            _innerClockMoveTime = 0f;
        }

        UpdateClockArrowRotation();
    }

    void UpdateClockArrowRotation()
    {
        _clockArrowGo.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (GetRatioValue(_innerClockMoveTime, _clockMoveTime) * 360f)));
    }

    #endregion

    #region Timer

    public void SetTimerAndStart(float time)
    {
        _innerTimerTime = time;
        _timerTickTockGoing = true;
    }

    void TimerTimeTickTock()
    {
        _innerTimerTime -= Time.deltaTime;
        if (_innerTimerTime <= 0f)
        {
            _timerTickTockGoing = false;
            _innerTimerTime = 0f;
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        _timerGo.GetComponent<TextMesh>().text = _innerTimerTime.ToString("N2");
    }

    #endregion

    float GetRatioValue(float targetValue, float motherValue)
    {
        return targetValue / motherValue;
    }

}
