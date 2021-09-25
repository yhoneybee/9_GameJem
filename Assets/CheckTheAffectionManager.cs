using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheAffectionManager : MonoBehaviour
{
    [SerializeField] string _loadLoopScene;
    [SerializeField] int _maximumDays;

    [SerializeField] int _affectionMinimum = 30;
    [SerializeField] int _affectionMidle = 70;

    [SerializeField] bool _isSceneLoadStart = false;

    

    void Start()
    {
        _isSceneLoadStart = false;
        // 애정도 확인 for GameOver
        if (GameManager.Instance.Affection <= 0)
        {
            Debug.Log("애정도 ============= " + GameManager.Instance.Affection);
            LoadGameOverScene();
        }
        Debug.Log("오늘 날짜 ============ " + ListContainer.LC.GetNumberOfDay());
        
        // 6일, 즉 설정 날과 같거나 더 많이 진행 되었는가?
        if (ListContainer.LC.GetNumberOfDay() >= _maximumDays && _isSceneLoadStart == false)
        {
            if(GameManager.Instance.Affection <= _affectionMinimum)
            {
                Debug.Log("헤어짐 엔딩");
                ListContainer.LC.EndingNumber = 1;
                LoadSceneForShowEnding();
            }
            else if(GameManager.Instance.Affection <= _affectionMidle && GameManager.Instance.Affection > _affectionMinimum)
            {
                Debug.Log("병명 들킴 엔딩");
                ListContainer.LC.EndingNumber = 2;
                LoadSceneForShowEnding();
            }
            else if(GameManager.Instance.Affection > _affectionMidle)
            {
                Debug.Log("결혼 엔딩");
                ListContainer.LC.EndingNumber = 3;
                LoadSceneForShowEnding();
            }
        }
        else
        {
            LoadSceneForLoof();
        }
    }

    void LoadGameOverScene()
    {
        Debug.Log("GameOver");
        ListContainer.LC.EndingNumber = 0;
        LoadSceneForShowEnding();

    }

    void LoadSceneForShowEnding()
    {
        if (_isSceneLoadStart == true)
            return;
        SceneMananagementClass.SMC.LoadSceneAsSync("EndingScene");
        SceneMananagementClass.SMC.UnLoadSceneAsSync("CheckTheAffectionScene");
        _isSceneLoadStart = true;
    }

    void LoadSceneForLoof()
    {
        SceneMananagementClass.SMC.LoadSceneAsSync(_loadLoopScene);
        SceneMananagementClass.SMC.UnLoadSceneAsSync("CheckTheAffectionScene");
    }



}
