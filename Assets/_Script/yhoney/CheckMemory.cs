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
    [SerializeField] RectTransform SecRotate;
    [SerializeField] RectTransform QuestionObj;
    [SerializeField] RectTransform ScrollView;

    [SerializeField] Chatting Chatting;

    Dictionary<string, string[]> Choice = new Dictionary<string, string[]>()
    {
        { "��ȭ��", new string[] { "ȣ��","�ִϸ��̼�","���̽�","��Ÿ��" } },
        { "�������", new string[] { "�Ľ�Ÿ","������ũ","��Ʃ","��" } },
        { "����ī��", new string[] { "�����","��","��","����" } },
        { "����", new string[] { "���","������","�����","�Ķ���" } },
        { "������", new string[] { "����","����","��ũ","������ ����" } },
        { "�Ǽ�������", new string[] { "���ڰ�","�Ҳ�","�����","�ذ�" } },
    };

    Dictionary<string, string[]> Questions = new Dictionary<string, string[]>()
    {
        { "��ȭ��", new string[] { "��ȭ ���� ��ȭ�� ���� ���߾�. ���� �� �帣�� �� �¾Ҿ�?","��ȭ ���µ� �����̴���, �װ� �׷��� ����?","���ϵ� ������ ���� �帣 ��ȭ����? �� �帣 �����ϴ� �� ������" } },
        { "�������", new string[] { "����������� ���� ������ �Կ� �� �¾Ҿ�?", "���� �츮 ����������� ������ �ʹ� ���� �Ծ���..", "���� �װ� �׷��� �� ���� �Դ� �� ó�� �þ�." } },
        { "����ī��", new string[] { "�װ� ������ �׷��� �����ϴ� �� ������.", "����ī�信�� ���� ���� ���ٵ��� ������ ������?", "�ʴ� ����ī�信�� �� �ȹ�������?" } },
        { "����", new string[] { "�������� �� �ʿ� ���� ������ �ʹ� �ű����� �ʾ�?", "���� �������� �� ���� �� �˾ҳ���", "����� ���� �� ������ ���� �ɱ�?" } },
        { "������", new string[] { "���� ��¥ ���� ���ϴ���, ���� ���� �ȹ����� ��?", "�����ϴٰ� �� ��¦ ��ݾ� ����. �׷��� ��������?", "������ �� ���� ���� �޿��� ������ �� �˾��ݾ� �̤�" } },
        { "�Ǽ�������", new string[] { "�׼����� ���� �� ���� ��¦�Ÿ����� �װ� �����̾�?", "�ʰ� ��û �ڼ��� ���� �׼����� �����׵� �� ��︱��?", "���� �� �Ǽ��縮������ �� ������ ���ڰ��� �ʴ� ���?" } },
    };

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
        get { return left_time; }
        set
        {
            left_time = value;
            SecRotate.rotation = Quaternion.AngleAxis(-72 * left_time, Vector3.forward);
        }
    }
    private int question_count = 5;
    public int QuestionCount
    {
        get { return question_count; }
        set
        {
            question_count = value;
            if (question_count < 0)
            {
                // TODO : END
                HideQ();
            }
        }
    }

    private void Awake()
    {
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
                HideQ();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Question($"���� ������ �Ծ���?", new string[] { "����", "ġŲ", "¥���", "«��" });
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
        if (question_count <= 0) return;

        Chatting.Chat(question, false);
        Chatting.Chat("", true);


        for (int i = 0; i < 4; i++) Answers[i].text = answer[i];

        AppearQ();

        IsTimer = true;
        --QuestionCount;
    }
    public void Answer(int index)
    {
        if (LeftTime < 5)
        {
            // TODO : Presentation�� �����ؼ� bool�� true���(�ٲ���ٸ�)
            // Ȧ�� ���� �ƴ϶�� ¦�� ����

            HideQ();
            Chatting.MessageStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text = $"���";
        }
    }

    public void HideQ()
    {
        ScrollView.anchoredPosition = new Vector2(0, 0);
        ScrollView.sizeDelta = new Vector2(ScrollView.sizeDelta.x, 1080);
        Chatting.SetZero();
        QuestionObj.gameObject.SetActive(false);
    }

    public void AppearQ()
    {
        ScrollView.anchoredPosition = new Vector2(0, 211.75f);
        ScrollView.sizeDelta = new Vector2(ScrollView.sizeDelta.x, 656.5f);
        Chatting.SetZero();
        QuestionObj.gameObject.SetActive(true);
    }
}
