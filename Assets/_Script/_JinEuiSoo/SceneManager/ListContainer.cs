using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContainer : MonoBehaviour
{
    private static ListContainer _listContainer;

    /// <summary>
    /// ListContainer
    /// </summary>
    public static ListContainer LC
    {
        get
        {
            if (_listContainer == null)
            {
                ListContainer obj = FindObjectOfType<ListContainer>();

                if (obj != null)
                {
                    _listContainer = obj;
                }
                else
                {
                    var newSingleton = new GameObject("ListContainer").AddComponent<ListContainer>();
                    _listContainer = newSingleton;
                }
            }
            return _listContainer;
        }
    }

    List<string> _presentationResult = new List<string>();
    public List<string> PresentationResult => _presentationResult;

    private void Awake()
    {
        #region Singleton Instantiate
        var objs = FindObjectsOfType<ListContainer>();
        if (objs.Length > 1)
        {
            Debug.LogError("New GameRullManager Added And Destroy Automatically");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
}
