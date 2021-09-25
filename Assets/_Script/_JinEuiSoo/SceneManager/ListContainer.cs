using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContainer : MonoBehaviour
{
    private static ListContainer _listContainer;

    [SerializeField] int _numberOfDay = 0;
    [SerializeField] int _endingNumber = 0;
    public int EndingNumber
    {
        get => _endingNumber;
        set => _endingNumber = value;
    }

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

    List<ChangeThingInfoStr> _presentationResult = new List<ChangeThingInfoStr>();
    public List<ChangeThingInfoStr> PresentationResult => _presentationResult;

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

    public void AddPresentationResult(ChangeThingInfoStr[] results)
    {
        foreach (ChangeThingInfoStr tempString in results)
        {
            _presentationResult.Add(tempString);
        }
    }

    public void AddNumberOfDay(int number) => _numberOfDay += number;
    public int GetNumberOfDay() => _numberOfDay;
    public void SetToZeroNumberOfDay() => _numberOfDay = 0;
    public void ClearPresentationResultList() => _presentationResult.Clear();
}
