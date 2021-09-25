using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultControlObject", menuName = "Scriptable Object/DifficultControlObject")]
[System.Serializable]
public class SCO_DifficultControlOption : ScriptableObject
{
    [Range(0f, 1f)]
    [SerializeField] float _chanceForBadGirlActive = 0.25f;
    [Range(10f, 24f)]
    [SerializeField] float _aTimeForDate = 24f;
    [Range(0f, 14f)]
    [SerializeField] float _aTimeForDateRandomRange = 0f;
    [Range(3, 6)]
    [SerializeField] int _visitingPlacesForDate = 3;
    [Range(0, 3)]
    [SerializeField] int _visitingPlacesForDateRandomRange = 0;


    public float ChanceForBadGirlActive => _chanceForBadGirlActive;

    public float ATimeForDate
    {
        get
        {
            float tempfloatATimeForDate;

            tempfloatATimeForDate =
                Random.Range(_aTimeForDate - _aTimeForDateRandomRange,
                             _aTimeForDate + _aTimeForDateRandomRange);

            return Mathf.Clamp(tempfloatATimeForDate, 10f, 24f);
        }
    }

    public int VisitingPlacesForDate
    {
        get
        {
            int tempIntVisitingPlacesNumber;

            tempIntVisitingPlacesNumber =
                Random.Range(_visitingPlacesForDate - _visitingPlacesForDateRandomRange,
                             _visitingPlacesForDate + _visitingPlacesForDateRandomRange);

            return Mathf.Clamp(tempIntVisitingPlacesNumber, 3, 6);
        }
    }
}
