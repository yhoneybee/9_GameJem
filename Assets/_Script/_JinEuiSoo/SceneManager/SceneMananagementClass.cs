using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMananagementClass : MonoBehaviour
{
    [SerializeField] string _firstLoadScene;
    [SerializeField] string _secondLoadScene;

    [SerializeField] bool _test;
    [SerializeField] string _loadSceneTestString;

    [SerializeField] AudioStorage _lobbyScene;
    [SerializeField] AudioStorage _ImageScene;
    [SerializeField] AudioStorage _presentationScene;
    [SerializeField] AudioStorage _messageCheckingScene;
    [SerializeField] AudioStorage _checkTheAffectionScene;
    [SerializeField] AudioStorage _endingBadScene;
    [SerializeField] AudioStorage _endingModerateScene;
    [SerializeField] AudioStorage _endingGoodScene;

    #region SceneTransition
    [SerializeField] float _waitTimeForTransition = 0.3f;
    [SerializeField] float _timeForWaitTimeBetweenTransition = 0.5f;
    [SerializeField] float _timeForPlayTransition = 0.5f;
    [SerializeField] float _timeForWaitBeforeSceneStart = 1.0f;
    [SerializeField] Image _transitionImage;
    #endregion


    private static SceneMananagementClass _sceneManagementClass;

    /// <summary>
    /// SceneMananagementClass
    /// </summary>
    public static SceneMananagementClass SMC
    {
        get
        {
            if (_sceneManagementClass == null)
            {
                SceneMananagementClass obj = FindObjectOfType<SceneMananagementClass>();

                if (obj != null)
                {
                    _sceneManagementClass = obj;
                }
                else
                {
                    var newSingleton = new GameObject("SceneManager").AddComponent<SceneMananagementClass>();
                    _sceneManagementClass = newSingleton;
                }
            }
            return _sceneManagementClass;
        }
    }

    private void Awake()
    {
        #region Singleton Instantiate
        var objs = FindObjectsOfType<SceneMananagementClass>();
        if (objs.Length > 1)
        {
            Debug.LogError("New SceneMananagementClass Added And Destroy Automatically");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }

    private void Start()
    {
        LoadSceneAsSync(_firstLoadScene);
    }

    private void Update()
    {
        if(_test)
        {
            _test = false;
            LoadSceneAsSync(_loadSceneTestString);
        }
    }

    public void UnLoadSceneAsSync(string sceneName)
    {
        StartCoroutine(UnLoadSceneWaitIE(sceneName));

    }

    public void LoadSceneAsSync(string sceneName)
    {
        StartCoroutine(LoadSceneWaitIE(sceneName));
        StartCoroutine(DisplayTransitionToBlack(sceneName));
    }

    IEnumerator LoadSceneWaitIE(string sceneName)
    {
        yield return new WaitForSeconds(_waitTimeForTransition);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    IEnumerator UnLoadSceneWaitIE(string sceneName)
    {
        yield return new WaitForSeconds(_waitTimeForTransition);
        SceneManager.UnloadSceneAsync(sceneName);
    }

    IEnumerator DisplayTransitionToBlack(string sceneName)
    {
        float tempFloatInnerTimeForTransition = _timeForPlayTransition;
        for(int ia = 0; ia < 300; ia++)
        {
            yield return new WaitForEndOfFrame();
            tempFloatInnerTimeForTransition -= Time.deltaTime;

            float tempFloatA = GetReflectValue(tempFloatInnerTimeForTransition, 0f, _timeForPlayTransition);

            SetSpritRendereAlpha(tempFloatA);

            if (tempFloatInnerTimeForTransition <= 0f)
                break;
        }
        PlayAudioWeirdly(sceneName);
        StartCoroutine(DisplayTransitionToWhite(sceneName));
        SetSpritRendereAlpha(1f);
    }

    IEnumerator DisplayTransitionToWhite(string sceneName)
    {
        SetSpritRendereAlpha(1f);
        yield return new WaitForSeconds(_timeForWaitTimeBetweenTransition);

        float tempFloatInnerTimeForTransition = _timeForPlayTransition;
        for (int ia = 0; ia < 300; ia++)
        {
            yield return new WaitForEndOfFrame();
            tempFloatInnerTimeForTransition -= Time.deltaTime;

            float tempFloatA = tempFloatInnerTimeForTransition / _timeForPlayTransition;

            SetSpritRendereAlpha(tempFloatA);

            if (tempFloatInnerTimeForTransition <= 0f)
                break;
        }
    }

    void SetSpritRendereAlpha(float alpha)
    {
        Debug.Log("Alpha : " + alpha);
        if(alpha == float.NaN)
        {
            alpha = 1f;
        }
        _transitionImage.color = new Color(_transitionImage.color.r, _transitionImage.color.g, _transitionImage.color.b, alpha);

    }

    void PlayAudioWeirdly(string sceneName)
    {
        switch(sceneName)
        {
            default:
            case "LobbyScene":
                SoundManager.SM.RequestPlayBGM(_lobbyScene.name);
                ListContainer.LC.SetToZeroNumberOfDay();
                break;
            case "IngameScene":
                SoundManager.SM.RequestPlayBGM(_ImageScene.name);
                break;
            case "PicturePresentating":
                SoundManager.SM.RequestPlayBGM(_presentationScene.name);
                ListContainer.LC.AddNumberOfDay(1);
                break;
            case "CheckMemory":
                SoundManager.SM.RequestPlayBGM(_messageCheckingScene.name);
                ListContainer.LC.AddNumberOfDay(1);
                StartCoroutine(WaitTransitionCheckMemoryStart());
                break;
            case "CheckTheAffectionScene":
                break;
        }
    }

    IEnumerator WaitTransitionCheckMemoryStart()
    {
        yield return new WaitForSeconds(_timeForWaitBeforeSceneStart);
        GameObject.Find("CheckMemory").GetComponent<CheckMemory>().StartCheckMemory();
    }

    float GetReflectValue(float value, float minRange, float maxRange)
    {
        // Set Retrun Checker before start to calculate for defending error occure
        if (value < minRange || value > maxRange || minRange > maxRange)
            return float.NaN;

        // Value의 백분율을 구합니다.
        float tempFloatRatioOfValue = GetRatioValue(value - minRange, maxRange - minRange);

        // 이를 반전합니다.
        if (tempFloatRatioOfValue > 0.5f)
            return 1f - tempFloatRatioOfValue;
        else if (tempFloatRatioOfValue < 0.5f)
            return Mathf.Abs(tempFloatRatioOfValue - 1f);
        else
            return tempFloatRatioOfValue;
    }

    float GetRatioValue(float targetValue, float motherValue)
    {
        return targetValue / motherValue;
    }    
}
