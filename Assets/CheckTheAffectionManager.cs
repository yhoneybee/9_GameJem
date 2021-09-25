using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheAffectionManager : MonoBehaviour
{
    [SerializeField] string _roadRoopScene;
    [SerializeField] int _maximumDays;

    [SerializeField] int _affectionMinimum = 30;
    [SerializeField] int _affectionMidle = 70;

    

    void Start()
    {

        // ������ Ȯ�� for GameOver
        if(GameManager.Instance.Affection <= 0)
        {
            Debug.Log("������ ============= " + GameManager.Instance.Affection);
            LoadGameOverScene();
        }
        Debug.Log("���� ��¥ ============ " + ListContainer.LC.GetNumberOfDay());
        
        // 6��, �� ���� ���� ���ų� �� ���� ���� �Ǿ��°�?
        if (ListContainer.LC.GetNumberOfDay() >= _maximumDays)
        {
            if(GameManager.Instance.Affection <= _affectionMinimum)
            {
                Debug.Log("����� ����");
            }
            else if(GameManager.Instance.Affection <= _affectionMidle && GameManager.Instance.Affection > _affectionMinimum)
            {
                Debug.Log("���� ��Ŵ ����");
            }
            else if(GameManager.Instance.Affection > _affectionMidle)
            {
                Debug.Log("��ȥ ����");
            }
        }


        SceneMananagementClass.SMC.LoadSceneAsSync(_roadRoopScene);
        SceneMananagementClass.SMC.UnLoadSceneAsSync("CheckTheAffectionScene");
    }

    void LoadGameOverScene()
    {
        Debug.Log("GameOver");
        // SceneMananagementClass.SMC.LoadSceneAsSync(_roadRoopScene);
        // SceneMananagementClass.SMC.UnLoadSceneAsSync("CheckTheAffectionScene");
    }



}
