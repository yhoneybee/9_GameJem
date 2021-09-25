using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePictureShowerObject : MonoBehaviour
{
    [SerializeField] string _presentationName = "Presentation";

    [SerializeField] string _placePictureName = "BasicScene";

    [SerializeField] GameObject[] _changeAblesThings;
    [SerializeField] GameObject[] _badGirlPositions;
    [SerializeField] public bool _isBadGirlActive;

    ScenePictureShower_Presentation _presentation;

    private void Start()
    {
        GetInitializeValuesFromPresentation();

        // Set GameObjectChangeAbles
        GameObject tempGameObjectChangeAblesThing = _changeAblesThings[Random.Range(0, _changeAblesThings.Length)];
        tempGameObjectChangeAblesThing.SetActive(true);

        // Set BadGirlActive
        if(_isBadGirlActive)
        {
            _badGirlPositions[Random.Range(0, _badGirlPositions.Length)].SetActive(true);
        }

        ReportToPresentationThatChangeThingInfo(tempGameObjectChangeAblesThing.name);
    }

    void GetInitializeValuesFromPresentation()
    {
        _presentation = GameObject.Find(_presentationName).GetComponent<ScenePictureShower_Presentation>();
        _isBadGirlActive = _presentation.IsBadGirlActive;
    }

    void ReportToPresentationThatChangeThingInfo(string rightMenu)
    {
        ChangeThingInfoStr tempThingInfo = new ChangeThingInfoStr();
        tempThingInfo.IsModifedByBadGirl = _isBadGirlActive;
        tempThingInfo.PlaceAndActionStringArr = new string[4];
        tempThingInfo.PlaceAndActionStringArr[0] = _placePictureName;
        tempThingInfo.PlaceAndActionStringArr[1] = _presentation.PlaceNames[Random.Range(0, _presentation.PlaceNames.Length)];
        tempThingInfo.PlaceAndActionStringArr[2] = rightMenu;
        tempThingInfo.PlaceAndActionStringArr[3] = _changeAblesThings[Random.Range(0, _changeAblesThings.Length)].name;
        tempThingInfo.OrderOfVisitingPlace = _presentation.OrderOfVisiting;


        // Wrong Place name correction. .. If the name is same as right place, wrong place name change to another place name.
        if (tempThingInfo.PlaceAndActionStringArr[0] == tempThingInfo.PlaceAndActionStringArr[1])
        {
            for(int a = 0; a < 100; a ++)
            {
                tempThingInfo.PlaceAndActionStringArr[1] = _presentation.PlaceNames[Random.Range(0, _presentation.PlaceNames.Length)];

                if (tempThingInfo.PlaceAndActionStringArr[0] != tempThingInfo.PlaceAndActionStringArr[1])
                    break;
            }
        }


        // Report
        _presentation.AddChangeThingsInfo(tempThingInfo);
    }

    #region Interface

    #endregion

}
