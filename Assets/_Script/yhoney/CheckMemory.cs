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
        // TODO : 여기서 Presentation에 접근해서 갱신
        int year = 2021, month = 1, day = 1;
        Date.text = $"{year} / {month} / {day}";
        Content.text = $"{month}월 {day}일 입니다~";
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
            // TODO : Presentation에 접근해서 bool이 true라면(바뀌었다면)
            // 홀수 정답
            // 아니라면 짝수 정답
        }
    }
}
