using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutScene : MonoBehaviour
{
    public List<GameObject> ImageList = new List<GameObject>();
    int index = 0;

    [SerializeField] float _timeForPlay;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ImageList.Count; i++)
            ImageList[i].SetActive(false);

        InvokeRepeating("CutUpdate", 0, _timeForPlay);
    }
    void CutUpdate()
    {
        if (index < ImageList.Count)
        {
            ImageList[index].SetActive(true);
            try
            {
                ImageList[index-1].SetActive(false);
            }
            catch
            {

            }
            index++;
        }
        else
        {
            CancelInvoke("CutUpdate");
            for (int i = 0; i < ImageList.Count; i++)
                ImageList[i].SetActive(false);
        }
    }
}
