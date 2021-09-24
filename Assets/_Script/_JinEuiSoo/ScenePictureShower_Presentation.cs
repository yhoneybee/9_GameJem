using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePictureShower_Presentation : MonoBehaviour
{

    [SerializeField] List<ChangeThingInfoStr> _changeThingsInfos = new List<ChangeThingInfoStr>();
    public List<ChangeThingInfoStr> ChangeThingsInfos => _changeThingsInfos;

    [SerializeField] int _aDayForVisitingPlace;
    [SerializeField] float _origineTime = 24f;
    [SerializeField] float _innerTimeForAllPicture = 0f;
    [SerializeField] float _innerTimeForAnPicture = 0f;

    [SerializeField] GameObject[] _pictures;


    #region Methoud

    // Show An Picture
    void PresentationShowing()
    {
        Instantiate(_pictures[Random.Range(0, _pictures.Length)]);
    }

    // Set The Picture for change Things
    // do that in Picture.

    // get the ChangeThingsInfo
    // do that in Picture.

    // Check the Time


    // If the time isn't yet, Wait.


    // If the time done, Show An Picture


    // If the time for 



    #endregion



    #region Interfaces

    public void AddChangeThingsInfo(ChangeThingInfoStr info) => _changeThingsInfos.Add(info);

    #endregion


}
