using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePictureShower_Presentation : MonoBehaviour
{

    [SerializeField] List<SCO_ChangeThingsInfo> _changeThingsInfos = new List<SCO_ChangeThingsInfo>();
    public List<SCO_ChangeThingsInfo> ChangeThingsInfos => _changeThingsInfos;
    
    [SerializeField] float _origineTime = 24f;
    [SerializeField] float _innerTimeForAllPicture = 0f;
    [SerializeField] float _innerTimeForAnPicture = 0f;

    [SerializeField] GameObject[] _pictures;


    #region Methoud

    // Show An Picture

    // Set The Picture for change Things

    // get the ChangeThingsInfo

    // Check the Time

    // If the time isn't yet, Wait.

    // If the time done, Show An Picture

    // If the time for 


    void PresentationShowing()
    {

    }
    #endregion



    #region Interfaces

    public void AddChangeThingsInfo(SCO_ChangeThingsInfo info) => _changeThingsInfos.Add(info);

    #endregion


}
