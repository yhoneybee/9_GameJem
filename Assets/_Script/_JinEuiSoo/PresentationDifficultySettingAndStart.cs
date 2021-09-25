using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationDifficultySettingAndStart : MonoBehaviour
{

    [SerializeField] bool _test;
    [SerializeField] GameObject _presentationGo;
    [SerializeField] int _difficulty;
    [SerializeField] SCO_DifficultControlOption[] _difficultyOption;

    [SerializeField] float _timeWaitForTransition = 1.0f;

    ScenePictureShower_Presentation _scenePictureShower;

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
