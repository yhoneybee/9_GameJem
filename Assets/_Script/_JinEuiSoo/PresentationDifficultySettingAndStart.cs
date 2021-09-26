using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationDifficultySettingAndStart : MonoBehaviour
{

    [SerializeField] bool _test;
    [SerializeField] GameObject _presentationGo;
    [SerializeField] int _difficulty;
    [SerializeField] int _todayNumber;
    [SerializeField] SCO_DifficultControlOption[] _difficultyOption;

    [SerializeField] float _timeWaitForTransition = 1.0f;

    ScenePictureShower_Presentation _scenePictureShower;

    private void Start()
    {
        _todayNumber = ListContainer.LC.GetNumberOfDay();
        switch(_todayNumber)
        {
            case 1:
                _difficulty = 0;
            break;
            case 3:
                _difficulty = 1;
            break;
            case 5:
                _difficulty = 2;
            break;
            case 7:
                _difficulty = 3;
            break;
            case 9:
                _difficulty = 4;
            break;
            case 11:
                _difficulty = 5;
            break;
            case 13:
                _difficulty = 6;
            break;
        }
    }

    private void Update()
    {
        if(_test)
        {
            _test = false;
            StartCoroutine(TimeWaitForTransition());
        }
    }

    IEnumerator TimeWaitForTransition()
    {
        yield return new WaitForSeconds(_timeWaitForTransition);
        PresentationSceneStart();
    }

    void PresentationSceneStart()
    {
        _scenePictureShower = _presentationGo.GetComponent<ScenePictureShower_Presentation>();
        _scenePictureShower.SetDifficultyByAnotherClass(_difficultyOption[_difficulty]);
        _scenePictureShower.PicturePresentationStart();
    }
}
