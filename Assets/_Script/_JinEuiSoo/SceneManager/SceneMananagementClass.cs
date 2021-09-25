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
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadSceneAsSync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
