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
        // ������ Ȯ�� for GameOver
        if (GameManager.Instance.Affection <= 0)
        {
            Debug.Log("������ ============= " + GameManager.Instance.Affection);
            LoadGameOverScene();
        }
        Debug.Log("���� ��¥ ============ " + ListContainer.LC.GetNumberOfDay());
        
        // 6��, �� ���� ���� ���ų� �� ���� ���� �Ǿ��°�?
        if (ListContainer.LC.GetNumberOfDay() >= _maximumDays && _isSceneLoadStart == false)
        {
            if(GameManager.Instance.Affection <= _affectionMinimum)
            {
                Debug.Log("����� ����");
                ListContainer.LC.EndingNumber = 1;
                LoadSceneForShowEnding();
            }
            else if(GameManager.Instance.Affection <= _affectionMidle && GameManager.Instance.Affection > _affectionMinimum)
            {
                Debug.Log("���� ��Ŵ ����");
                ListContainer.LC.EndingNumber = 2;
                LoadSceneForShowEnding();
            }
            else if(GameManager.Instance.Affection > _affectionMidle)
            {
                Debug.Log("��ȥ ����");
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
