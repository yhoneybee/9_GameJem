using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State
{
    GREETING,
    QUESTION,
    ANSWER,
    END,
}

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
        { "����ī��", new string[] { "������","��","��","����" } },
        { "����", new string[] { "���","������","�����","�Ķ���" } },
        { "������", new string[] { "����","����","��ũ","������ ����" } },
        { "�Ǽ�������", new string[] { "���ڰ�","�Ҳ�","�����","�ذ�" } },
    };

    Dictionary<string, string[]> Questions = new Dictionary<string, string[]>()
    {
        { "��ȭ��", new string[] { "��ȭ ���� ��ȭ�� ���� ���߾�. ���� �� �帣�� �� �¾Ҿ�?","��ȭ �����µ� �����̴���, �װ� �׷��� ����?","���ϵ� ������ ���� �帣 ��ȭ����? �� �帣 �����ϴ� �� ������" } },
        { "�������", new string[] { "����������� ���� ������ �Կ� �� �¾Ҿ�?", "���� �츮 ����������� ������ �ʹ� ���� �Ծ���..", "���� �װ� �׷��� �� ���� �Դ� �� ó�� �þ�." } },
        { "����ī��", new string[] { "�װ� ������ �׷��� �����ϴ� �� ������.", "����ī�信�� ���� ���� ���ٵ��� ������ ������?", "�ʴ� ����ī�信�� �� �ȹ�������?" } },
        { "����", new string[] { "�������� �� �ʿ� ���� ������ �ʹ� �ű����� �ʾ�?", "���� �������� �� ���� �� �˾ҳ���", "����� ���� �� ������ ���� �ɱ�?" } },
        { "������", new string[] { "���� ��¥ ���� ���ϴ���, ���� ���� �ȹ����� ��?", "�����ϴٰ� �� ��¦ ��ݾ� ����. �׷��� ��������?", "������ �� ���� ���� �޿��� ������ �� �˾��ݾ� �̤�" } },
        { "�Ǽ�������", new string[] { "�׼����� ���� �� ���� ��¦�Ÿ����� �װ� �����̾�?", "�ʰ� ��û �ڼ��� ���� �׼����� �����׵� �� ��︱��?", "���� �� �Ǽ��縮������ �� ������ ���ڰ��� �ʴ� ���?" } },
    };

    List<string[]> Greetings = new List<string[]>()
    {
        new string[] { "���� ����Ʈ�� �ʹ� ��ſ���", "��... ���� �ʹ� ���Ҿ�" },
        new string[] { "������ �� ����?", "���п� ������ ����" },
        new string[] { "���� �ʹ� �� ��Ҵ� ����", "�¾� ����" },
        new string[] { "���� �� ���?", "��, �ʹ� �ູ�� ���� ���" },
    };

    List<string> Positive = new List<string>()
    {
        "�� ������ �츮 ���߳�!",
        "�� �ʵ�? ����!",
        "���� ���� ����",
        "�������� �� ���� ���� ���ڳ�!����",
    };

    List<string> Negative = new List<string>()
    {
        ".........",
        "��....��..�׷���..?",
        "�� �ٸ� ����̶� �����ϰ� �Ƴ�?",
        "�װ� �� �ƴѵ�?",
    };

    List<string[]> End = new List<string[]>()
    {
        new string[] { "���ڰ� ���Ϻ�", "�� �޲�~" },
        new string[] { "���� ����Ʈ ���ȴ�", "���� ����!" },
        new string[] { "���� �� ������", "���� �� ��...��..��" },
        new string[] { "�� �޲�", "������ ��..�׷���" },
    };

    string GetString(string key, string select)
    {
        string result = "";
        switch (key)
        {
            case "��ȭ��":
                result = $"��ȭ������ �� {select} �帣 ��ȭ�� �ʹ� ��հ� ��ſ���!";
                break;
            case "�������":
                result = $"�� ������� {select} ���� ���־���.. �ٽ� ���� �;�!";
                break;
            case "����ī��":
                result = $"{select}�� �ʹ� �Ϳ�����!";
                break;
            case "����":
                result = $"{select}�� ���� �� �����ϳ���";
                break;
            case "������":
                result = $"{select}�� ���� �������� ��";
                break;
            case "�Ǽ�������":
                result = $"{select}����� �� �����̾���";
                break;
        }
        return result;
    }

    private State state;

    public State State
    {
        get { return state; }
        set
        {
            state = value;

            int rand = Random.Range(0, 4);

            switch (State)
            {
                case State.GREETING:
                    StartCoroutine(EDelay(new System.Tuple<string, bool>[] { new System.Tuple<string, bool>(Greetings[rand][0], false), new System.Tuple<string, bool>(Greetings[rand][1], true) }));
                    break;
                case State.QUESTION:
                    Question(Questions["��ȭ��"][Random.Range(0, 3)], Choice["��ȭ��"]);
                    break;
                case State.ANSWER:
                    break;
                case State.END:
                    StartCoroutine(EDelay(new System.Tuple<string, bool>[] { new System.Tuple<string, bool>(End[rand][0], false), new System.Tuple<string, bool>(End[rand][1], true) }));
                    break;
            }
        }
    }

    IEnumerator EDelay(params System.Tuple<string, bool>[] tuples)
    {
        foreach (var t in tuples)
        {
            yield return new WaitForSeconds(2);
            Chatting.Chat(t.Item1, t.Item2);
        }

        yield return new WaitForSeconds(2);

        switch (State)
        {
            case State.GREETING:
                State = State.QUESTION;
                break;
            case State.QUESTION:
                break;
            case State.END:
                break;
        }

        yield return null;
    }

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
                State = State.END;
            }
        }
    }

    private void Start()
    {
        State = State.GREETING;
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
                State = State.QUESTION;
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
        if (question_count <= 0)
        {
            State = State.END;
            return;
        }

        StartCoroutine(EDelay(new System.Tuple<string, bool>[] { new System.Tuple<string, bool>(question, false), new System.Tuple<string, bool>("", true) }));
        Invoke(nameof(AppearQ), 5);

        for (int i = 0; i < 4; i++) Answers[i].text = answer[i];

        --QuestionCount;
    }
    public void Answer(int index)
    {
        if (LeftTime < 5)
        {
            // TODO : Presentation�� �����ؼ� bool�� true���(�ٲ���ٸ�)
            // Ȧ�� ���� �ƴ϶�� ¦�� ����

            HideQ();

            int rand = Random.Range(0, 4);

            StartCoroutine(EDelay(new System.Tuple<string, bool>(Negative[rand], false)));
            //StartCoroutine(EDelay(new System.Tuple<string, bool>(Positive[rand], false)));

            Chatting.MessageStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text = GetString("��ȭ��", Choice["��ȭ��"][index]);
            State = State.ANSWER;
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
        IsTimer = true;
    }
}