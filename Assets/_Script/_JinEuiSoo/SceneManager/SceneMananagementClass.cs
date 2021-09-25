using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMananagementClass : MonoBehaviour
{
    [SerializeField] string _firstLoadScene;
    [SerializeField] string _secondLoadScene;

    [SerializeField] bool _test;
    [SerializeField] string _loadSceneTestString;

    private void Start()
    {
        SceneManager.LoadSceneAsync(_firstLoadScene, LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(_test)
        {
            _test = false;
            SceneManager.LoadSceneAsync(_loadSceneTestString, LoadSceneMode.Additive);
        }
    }
}
