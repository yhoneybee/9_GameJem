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
    [SerializeField] RectTransform SecRotate;

    private bool is_timer;
    public bool IsTimer
    {
        get { return is_timer; }
        set 
        { 
            is_timer = value;
            left_time = 0;
        }
    }
    private float left_time;
    public float LeftTime
    {
        get {  return left_time; }
        set
        {
            left_time = value;
            SecRotate.rotation = Quaternion.AngleAxis(-72 * left_time, Vector3.forward);
        }
    }
    private int question_count;
    public int QuestionCount
    {
        get {  return question_count; }
        set 
        { 
            question_count = value;
            if (question_count <= 0)
            {
                // TODO : END
            }
        }
    }

    private void Start()
    {
        IsTimer = true;
    }
    private void Update()
    {
        if (IsTimer)
        {
            LeftTime += Time.deltaTime;
            if (LeftTime > 5)
            {
                // TODO : Fail
                IsTimer = false;
            }
        }
    }

    public void NextMemo()
    {
        // TODO : ���⼭ Presentation�� �����ؼ� ���� �޸� ���ٸ� Text ���
        int year = 2021, month = 1, day = 1;
        Date.text = $"{year} / {month} / {day}";
        Content.text = $"{month}�� {day}�� �Դϴ�~";
    }
    public void Question(string question, string[] answer)
    {
        QuestionText.text = question;
        for (int i = 0; i < 4; i++) Answers[i].text = answer[i];
        IsTimer = true;
        --QuestionCount;
    }
    public void Answer(int index)
    {
        if (LeftTime < 5 && LeftTime != 0)
        {
            // TODO : Presentation�� �����ؼ� bool�� true���(�ٲ���ٸ�)
            // Ȧ�� ���� �ƴ϶�� ¦�� ����
        }
    }
}
