using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckMemory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Date;
    [SerializeField] TextMeshProUGUI Content;
    [SerializeField] TextMeshProUGUI[] Answers;
    [SerializeField] TextMeshProUGUI QuestionText;

    private bool is_timer;
    public bool IsTimer
    {
        get { return is_timer; }
        set 
        { 
            is_timer = value;
            time = 0;
        }
    }

    float time;

    private void Update()
    {
        if (IsTimer)
        {
            time += Time.deltaTime;
            if (time > 5)
            {
                // fail
            }
        }
    }

    public void NextMemo()
    {
        // TODO : ���⼭ Presentation�� �����ؼ� ����
        int year = 2021, month = 1, day = 1;
        Date.text = $"{year} / {month} / {day}";
        Content.text = $"{month}�� {day}�� �Դϴ�~";
    }

    public void Question(string question, string[] answer)
    {
        QuestionText.text = question;
        for (int i = 0; i < 4; i++) Answers[i].text = answer[i];
    }

    public void Answer(int index)
    {
        if (time < 5)
        {
            // TODO : Presentation�� �����ؼ� bool�� true���(�ٲ���ٸ�)
            // Ȧ�� ����
            // �ƴ϶�� ¦�� ����
        }
    }
}
