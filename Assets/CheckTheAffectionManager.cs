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
        if(GameManager.Instance.affection <= 0)
        {
            LoadGameOverScene();
        }

        // 6��, �� ���� ���� ���ų� �� ���� ���� �Ǿ��°�?
        if(ListContainer.LC.GetNumberOfDay() >= _maximumDays)
        {
            if(GameManager.Instance.affection <= _affectionMinimum)
            {
                Debug.Log("����� ����");
            }
            else if(GameManager.Instance.affection <= _affectionMidle && GameManager.Instance.affection > _affectionMinimum)
            {
                Debug.Log("���� ��Ŵ ����");
            }
            else if(GameManager.Instance.affection > _affectionMidle)
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
