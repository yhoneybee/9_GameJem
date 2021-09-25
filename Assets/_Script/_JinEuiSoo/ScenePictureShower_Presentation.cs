using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScenePictureShower_Presentation : MonoBehaviour
{

    [SerializeField] List<ChangeThingInfoStr> _changeThingsInfos = new List<ChangeThingInfoStr>();
    public List<ChangeThingInfoStr> ChangeThingsInfos => _changeThingsInfos;

    [SerializeField] bool _test;
    [SerializeField] string _testBGM;
    [SerializeField] float _datingTime = 6f;
    
    #region Modifie area
    [Header("Modifie Area")]
    [SerializeField] int _visitingPlaceInADay = 3;

    [SerializeField] GameObject[] _picturePrefabs;
    [SerializeField] string[] _placeNames;
    public string[] PlaceNames => _placeNames;

    [SerializeField] string[] _actionNames;
    public string[] ActionNames => _actionNames;

    #endregion

    [Header("UIs")]
    #region UI Control

    [SerializeField] GameObject _UI_clock;

    #endregion

    #region Don't Modifie area
    [Header("Debug Area. Do not modife")]
    [SerializeField] bool _innerTimeForAllPictureTimeDecreasing;
    [SerializeField] bool _innerTimeForAnPictureTimeDecreasing;

    [SerializeField] float _origineTime = 24f;
    [SerializeField] float _pictureShowingTime = 8f;
    [SerializeField] float _innerTimeForAllPicture = 0f;
    [SerializeField] float _innerTimeForAnPicture = 0f;

    [SerializeField] int _currentDay = 0;
    [SerializeField] int _orderOfVisiting = 0;
    public int OrderOfVisiting => _orderOfVisiting;

    [SerializeField] bool _isBadGirlActive;
    public bool IsBadGirlActive => _isBadGirlActive;

    [SerializeField] float _badGirlActiveChance;

    [SerializeField] GameObject _anPicture;
    #endregion




    private void FixedUpdate()
    {
        if(_test)
        {
            _test = false;
            SoundManager.SM.RequestPlayBGM(_testBGM, true);
            // PicturePresentationTestStart(_datingTime);
        }
        TimeCheckings();
    }

    void TimeCheckings()
    {
        if (_innerTimeForAllPictureTimeDecreasing == true)
        {
            _innerTimeForAllPicture -= Time.deltaTime;
            CheckPicturePresentingTimeDone();
        }

        if (_innerTimeForAnPictureTimeDecreasing == true)
        {
            _innerTimeForAnPicture -= Time.deltaTime;
            CheckInnerTimeAnPicture();
        }
    }


    #region Functions For presentation

    public void SetDifficultyByAnotherClass(SCO_DifficultControlOption difficultOption)
    {
        _datingTime = difficultOption.ATimeForDate;
        _visitingPlaceInADay = difficultOption.VisitingPlacesForDate;
        _badGirlActiveChance = difficultOption.ChanceForBadGirlActive;
    }

    public void PicturePresentationTestStart(float datingTime)
    {
        _origineTime = datingTime;
        PictureShowingEventFirstStart();
    }

    public void PicturePresentationStart()
    {
        _origineTime = _datingTime;
        PictureShowingEventFirstStart();
    }

    // First Action
    void PictureShowingEventFirstStart()
    {

        ListContainer.LC.ClearPresentationResultList();
        // Initialize for Presentation
        _orderOfVisiting = 0;
        _pictureShowingTime = _origineTime / _visitingPlaceInADay;


        _innerTimeForAllPicture = _origineTime;
        _innerTimeForAnPicture = _pictureShowingTime;
        _innerTimeForAllPictureTimeDecreasing = true;
        _innerTimeForAnPictureTimeDecreasing = true;


        PresentationShowing();
        UI_ClockTimerStart();
    }
    

    // Show An Picture
    void PresentationShowing()
    {
        _anPicture = Instantiate(_picturePrefabs[UnityEngine.Random.Range(0, _picturePrefabs.Length)]);
        _orderOfVisiting++;
        SetBadGrilActive();
        UI_ClockStart();
    }


    void SetBadGrilActive()
    {
        float tempFloatRandomCount = UnityEngine.Random.Range(0f, 1f);
        Debug.Log("tempfloatRandomCount : " + tempFloatRandomCount);
        if (tempFloatRandomCount <= _badGirlActiveChance)
        {
            _isBadGirlActive = true;
        }
        else
        {
            _isBadGirlActive = false;
        }

    }

    // Set The Picture for change Things
    // do that in Picture.

    // get the ChangeThingsInfo
    // do that in Picture.


    // If the time isn't yet, Wait.
    void CheckInnerTimeAnPicture()
    {
        if(_innerTimeForAnPicture <= 0f)
        {
            _innerTimeForAnPictureTimeDecreasing = false;
            Destroy(_anPicture);
            UI_ClockClose();
            PictureShowingEventRestart();
        }
    }


    // If the time done, Show An Picture
    void PictureShowingEventRestart()
    {
        _innerTimeForAnPicture = _pictureShowingTime;
        _innerTimeForAnPictureTimeDecreasing = true;
        PresentationShowing();
    }


    // If the time for day done, Send Log.
    void CheckPicturePresentingTimeDone()
    {
        if(_innerTimeForAllPicture <= 0f)
        {
            DonePicturePresentingTime();
        }
    }

    void DonePicturePresentingTime()
    {
        _innerTimeForAllPictureTimeDecreasing = false;
        _innerTimeForAnPictureTimeDecreasing = false;
        _innerTimeForAllPicture = 0f;
        _innerTimeForAnPicture = 0f;
        _orderOfVisiting = 0;

        try
        {
            Destroy(_anPicture);
        }
        catch(NullReferenceException)
        {
            Debug.Log("Picture already gone");
        }


        Debug.Log("Presenting Time be done");

        foreach(ChangeThingInfoStr thing in _changeThingsInfos)
        {
            Debug.Log($"오늘은 몇번째 날? : {thing.NumberOfDay}, 몇 번째로 간 장소? : {thing.OrderOfVisitingPlace}");
            Debug.Log($"나쁜년이 활동 했나요? : {((thing.IsModifedByBadGirl) ? true : false)}");
            Debug.Log($"실제 장소 : {thing.PlaceAndActionStringArr[0]}, 실제 행동 : {thing.PlaceAndActionStringArr[2]}");
            Debug.Log($"잘못된 장소 : {thing.PlaceAndActionStringArr[1]}, 잘못된 행동 : {thing.PlaceAndActionStringArr[3]}");
        }

        ChangeThingInfoStr[] tempStringResult;
        tempStringResult = new ChangeThingInfoStr[_changeThingsInfos.Count];
        for(int ia = 0; ia < _changeThingsInfos.Count; ia++)
        {
            tempStringResult[ia] = _changeThingsInfos[ia];
        }

        ListContainer.LC.AddPresentationResult(tempStringResult);

        _changeThingsInfos.Clear();



        SceneMananagementClass.SMC.LoadSceneAsSync("CheckMemory");
        SceneMananagementClass.SMC.UnLoadSceneAsSync("PicturePresentating");
    }


    #endregion

    #region UI_ClockControl

    void UI_ClockStart()
    {
        _UI_clock.GetComponent<UI_ScenePresentationClock>().SetClockTimeAndStart(_pictureShowingTime);
    }

    void UI_ClockClose()
    {
        _UI_clock.GetComponent<UI_ScenePresentationClock>().SetClockStopAndInitialize();
    }

    void UI_ClockTimerStart()
    {
        _UI_clock.GetComponent<UI_ScenePresentationClock>().SetTimerAndStart(_origineTime);
    }

    #endregion

    #region Interfaces

    public void AddChangeThingsInfo(ChangeThingInfoStr info) => _changeThingsInfos.Add(info);

    #endregion


}
