using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum ClipPackNumber
    {
        ClipPackA,
        ClipPackB,
        ClipPackETC
    }

    private static SoundManager _soundManager;

    /// <summary>
    /// SoundManager
    /// </summary>
    public static SoundManager SM
    {
        get
        {
            if (_soundManager == null)
            {
                SoundManager obj = FindObjectOfType<SoundManager>();

                if (obj != null)
                {
                    _soundManager = obj;
                }
                else
                {
                    var newSingleton = new GameObject("SoundManager").AddComponent<SoundManager>();
                    _soundManager = newSingleton;
                }
            }
            return _soundManager;
        }
    }

    private void Awake()
    {
        #region Singleton Instantiate
        var objs = FindObjectsOfType<SoundManager>();
        if (objs.Length > 1)
        {
            Debug.LogError("New SoundManager Added And Destroy Automatically");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }

    [SerializeField] AudioStorage[] _BGMS, _clipPackA;
    [SerializeField] bool _test, _initialization;
    [SerializeField] string _testName;

    [SerializeField] GameObject _objectPoolingPrefab;

    [SerializeField] GameObject _BGMPool, _ambiencePool, _clipPool;

    [SerializeField] int _currentBGMNumber;
    [SerializeField] int _currentAmbienceNumber;
    [SerializeField] int _currentClipNumber;

    [SerializeField] int _howManyUseBGMSoundObjects = 8;
    [SerializeField] GameObject[] _BGMSoundObjectsPool;

    [SerializeField] int _howManyUseAmbienceSoundObjects = 32;
    [SerializeField] GameObject[] _ambienceSoundObjectsPool;

    [SerializeField] int _howManyUseClipSoundObjects = 128;
    [SerializeField] GameObject[] _clipSoundObjectsPool;

    private void Start()
    {
        _BGMPool = new GameObject();
        _ambiencePool = new GameObject();
        _clipPool = new GameObject();

        _BGMPool.name = "BGMPool";
        _ambiencePool.name = "AmbiencePool";
        _clipPool.name = "ClipPool";

        _BGMPool.transform.parent = this.gameObject.transform;
        _ambiencePool.transform.parent = this.gameObject.transform;
        _clipPool.transform.parent = this.gameObject.transform;

        _BGMSoundObjectsPool = new GameObject[_howManyUseBGMSoundObjects];
        _BGMSoundObjectsPool = InstantiateSoundPoolingGameObjects(_howManyUseBGMSoundObjects, _BGMPool.transform);

        _ambienceSoundObjectsPool = new GameObject[_howManyUseAmbienceSoundObjects];
        _ambienceSoundObjectsPool = InstantiateSoundPoolingGameObjects(_howManyUseAmbienceSoundObjects, _ambiencePool.transform);

        _clipSoundObjectsPool = new GameObject[_howManyUseClipSoundObjects];
        _clipSoundObjectsPool = InstantiateSoundPoolingGameObjects(_howManyUseClipSoundObjects, _clipPool.transform);

        SetSoundPoolTag(_BGMSoundObjectsPool, _howManyUseBGMSoundObjects, SoundPoolTag.BGM);
        SetSoundPoolTag(_ambienceSoundObjectsPool, _howManyUseAmbienceSoundObjects, SoundPoolTag.Ambience);
        SetSoundPoolTag(_clipSoundObjectsPool, _howManyUseClipSoundObjects, SoundPoolTag.Clip);
    }

    void SetSoundPoolTag(GameObject[] whatGameObjects, int howMany, SoundPoolTag tag)
    {
        for (int ai = 0; ai < howMany; ai++)
        {
            whatGameObjects[ai].GetComponent<BasicSoundClipPlay_Common>().SetSoundPoolTag(tag);
        }
    }

    GameObject[] InstantiateSoundPoolingGameObjects(int howMany, Transform setParent)
    {
        GameObject[] gameObjects = new GameObject[howMany];
        for(int ia = 0; ia < howMany; ia ++)
        {
            GameObject a = Instantiate(_objectPoolingPrefab, setParent);
            a.name = ia.ToString();
            gameObjects[ia] = a;
        }
        return gameObjects;
    }

    private void FixedUpdate()
    {
        if (_test)
        {
            RequestPlayClip(_testName);
            _test = false;
        }
    }

    public void RequestPlayBGM(string BGMName)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;
        for (int intA = 0; intA < _BGMS.Length; intA++)
        {
            if (_BGMS[intA].name == BGMName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
            }
        }

        if (nameFindBool)
        {
            Transform tempTransformFindPlayingChild = null;
            bool tempIsTransformFind = false;

            // 현재 재생중인 BGM이 있는지 확인한다.
            foreach(Transform target in _BGMPool.GetComponentInChildren<Transform>())
            {
                if(target.gameObject.activeSelf)
                {
                    tempTransformFindPlayingChild = target;
                    tempIsTransformFind = true;
                    break;
                }
            }

            // 현재 BGM을 실행시킨다.
            GameObject tempGameObject = GetBGMFromPool();
            tempGameObject.SetActive(true);
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_BGMS[playMusicNumberInt]);

            // 재생중인 BGM이 있다면, 그 BGM을 종료시키고, 다른 BGM을 실행하기 위해, 그 BGM을 리턴시킨다.
            if(tempIsTransformFind == true)
            {
                ClipRequestReturnToPool(tempTransformFindPlayingChild);
            }
        }
        else
        {
            Debug.Log("BGM Request decline. BGM name " + BGMName + " ins't insied of array");
        }
    }

    public void RequestPlayBGM(string BGMName, bool isLoop)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;
        for (int intA = 0; intA < _BGMS.Length; intA++)
        {
            if (_BGMS[intA].name == BGMName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
            }
        }

        if (nameFindBool)
        {
            Transform tempTransformFindPlayingChild = null;
            bool tempIsTransformFind = false;

            // 현재 재생중인 BGM이 있는지 확인한다.
            foreach (Transform target in _BGMPool.GetComponentInChildren<Transform>())
            {
                if (target.gameObject.activeSelf)
                {
                    tempTransformFindPlayingChild = target;
                    tempIsTransformFind = true;
                    break;
                }
            }

            // 현재 BGM을 실행시킨다. + Looping 기능도 들어간다.
            GameObject tempGameObject = GetBGMFromPool();
            tempGameObject.SetActive(true);
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_BGMS[playMusicNumberInt], isLoop);

            // 재생중인 BGM이 있다면, 그 BGM을 종료시키고, 다른 BGM을 실행하기 위해, 그 BGM을 리턴시킨다.
            if (tempIsTransformFind == true)
            {
                ClipRequestReturnToPool(tempTransformFindPlayingChild);
            }
        }
        else
        {
            Debug.Log("BGM Request decline. BGM name " + BGMName + " ins't insied of array");
        }
    }

    public void RequestPlayBGM(string BGMName, float playStartTime , bool isLoop)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;
        for (int intA = 0; intA < _BGMS.Length; intA++)
        {
            if (_BGMS[intA].name == BGMName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
            }
        }

        if (nameFindBool)
        {
            Transform tempTransformFindPlayingChild = null;
            bool tempIsTransformFind = false;

            // 현재 재생중인 BGM이 있는지 확인한다.
            foreach (Transform target in _BGMPool.GetComponentInChildren<Transform>())
            {
                if (target.gameObject.activeSelf)
                {
                    tempTransformFindPlayingChild = target;
                    tempIsTransformFind = true;
                    break;
                }
            }

            // 현재 BGM을 실행시킨다. + Looping 기능도 들어간다.
            GameObject tempGameObject = GetBGMFromPool();
            tempGameObject.SetActive(true);
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_BGMS[playMusicNumberInt], playStartTime, isLoop);

            // 재생중인 BGM이 있다면, 그 BGM을 종료시키고, 다른 BGM을 실행하기 위해, 그 BGM을 리턴시킨다.
            if (tempIsTransformFind == true)
            {
                ClipRequestReturnToPool(tempTransformFindPlayingChild);
            }
        }
        else
        {
            Debug.Log("BGM Request decline. BGM name " + BGMName + " ins't insied of array");
        }
    }

    internal void RequestPlayClip(string clipName)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;

        for (int intA = 0; intA < _clipPackA.Length; intA++)
        {
            if (_clipPackA[intA].name == clipName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
                break;
            }
        }

        if (nameFindBool)
        {
            // Set Basic object pooling setting.
            GameObject tempGameObject = GetClipFromPool();

            // Set sound Attributions.
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_clipPackA[playMusicNumberInt]);
        }
        else
        {
            Debug.Log("Clip Request decline. Clip name " + clipName + " ins't insied of array");
        }
    }

    internal void RequestPlayClip(string clipName, Vector3 pos)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;

        for (int intA = 0; intA < _clipPackA.Length; intA++)
        {
            if (_clipPackA[intA].name == clipName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
                break;
            }
        }

        if (nameFindBool)
        {
            // Set Basic object pooling setting.
            GameObject tempGameObject = GetClipFromPool();
            tempGameObject.transform.position = pos;

            // Set sound Attributions.
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_clipPackA[playMusicNumberInt]);
        }
        else
        {
            Debug.Log("Clip Request decline. Clip name " + clipName + " ins't insied of array");
        }
    }

    internal void RequestPlayClip(string clipName, float playStartTime, Vector3 pos)
    {
        bool nameFindBool = false;
        int playMusicNumberInt = 0;

        for (int intA = 0; intA < _clipPackA.Length; intA++)
        {
            if (_clipPackA[intA].name == clipName)
            {
                nameFindBool = true;
                playMusicNumberInt = intA;
                break;
            }
        }

        if (nameFindBool)
        {
            // Set Basic object pooling setting.
            GameObject tempGameObject = GetClipFromPool();
            tempGameObject.transform.position = pos;

            // Set sound Attributions.
            tempGameObject.GetComponent<BasicSoundClipPlay_Common>().SetInitialization(_clipPackA[playMusicNumberInt], playStartTime, false);
        }
        else
        {
            Debug.Log("Clip Request decline. Clip name " + clipName + " ins't insied of array");
        }
    }

    internal void ClipRequestReturnToPool(Transform thereSelves)
    {
        thereSelves.position = Vector3.zero;
        thereSelves.gameObject.SetActive(false);
    }

    internal GameObject GetBGMFromPool()
    {
        _currentBGMNumber++;
        if (_currentBGMNumber >= _howManyUseBGMSoundObjects)
            _currentBGMNumber = 0;
        _BGMSoundObjectsPool[_currentBGMNumber].SetActive(true);
        return _BGMSoundObjectsPool[_currentBGMNumber];
    }

    internal GameObject GetAmbienceFromPool()
    {
        _currentAmbienceNumber++;
        if (_currentAmbienceNumber >= _howManyUseAmbienceSoundObjects)
            _currentAmbienceNumber = 0;
        _ambienceSoundObjectsPool[_currentClipNumber].SetActive(true);
        return _ambienceSoundObjectsPool[_currentAmbienceNumber];
    }

    internal GameObject GetClipFromPool()
    {
        _currentClipNumber++;
        if (_currentClipNumber >= _howManyUseClipSoundObjects)
            _currentClipNumber = 0;
        _clipSoundObjectsPool[_currentClipNumber].SetActive(true);
        return _clipSoundObjectsPool[_currentClipNumber];
    }
}
