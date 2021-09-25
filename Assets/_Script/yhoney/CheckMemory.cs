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
        { "����ī��", new string[] { "�����","��","��","����" } },
        { "����", new string[] { "���","������","�����","�Ķ���" } },
        { "������", new string[] { "����","����","��ũ","������ ����" } },
        { "�Ǽ��縮��", new string[] { "���ڰ�","�Ҳ�","�����","�ذ�" } },
    };

    Dictionary<string, string[]> Questions = new Dictionary<string, string[]>()
    {
        { "��ȭ��", new string[] { "��ȭ ���� ��ȭ�� ���� ���߾�. ���� �� �帣�� �� �¾Ҿ�?","��ȭ ���µ� �����̴���, �װ� �׷��� ����?","���ϵ� ������ ���� �帣 ��ȭ����? �� �帣 �����ϴ� �� ������" } },
        { "�������", new string[] { "����������� ���� ������ �Կ� �� �¾Ҿ�?", "���� �츮 ����������� ������ �ʹ� ���� �Ծ���..", "���� �װ� �׷��� �� ���� �Դ� �� ó�� �þ�." } },
        { "����ī��", new string[] { "�װ� ������ �׷��� �����ϴ� �� ������.", "����ī�信�� ���� ���� ���ٵ��� ������ ������?", "�ʴ� ����ī�信�� �� �ȹ�������?" } },
        { "����", new string[] { "�������� �� �ʿ� ���� ������ �ʹ� �ű����� �ʾ�?", "���� �������� �� ���� �� �˾ҳ���", "����� ���� �� ������ ���� �ɱ�?" } },
        { "������", new string[] { "���� ��¥ ���� ���ϴ���, ���� ���� �ȹ����� ��?", "�����ϴٰ� �� ��¦ ��ݾ� ����. �׷��� ��������?", "������ �� ���� ���� �޿��� ������ �� �˾��ݾ� �̤�" } },
        { "�Ǽ��縮��", new string[] { "�׼����� ���� �� ���� ��¦�Ÿ����� �װ� �����̾�?", "�ʰ� ��û �ڼ��� ���� �׼����� �����׵� �� ��︱��?", "���� �� �Ǽ��縮������ �� ������ ���ڰ��� �ʴ� ���?" } },
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
        new string[] { "���� �� ������", "���� �� ��...��..�� zzZ" },
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
                result = $"{select} ���� �� �����ϳ���";
                break;
            case "������":
                result = $"{select}�� ���� �������� ��";
                break;
            case "�Ǽ��縮��":
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
                    isA = false;
                    if (q_idx == ListContainer.LC.PresentationResult.Count)
                    {
                        State = State.END;
                        break;
                    }
                    string str = ListContainer.LC.PresentationResult[q_idx].PlaceAndActionStringArr[0];
                    print(str);
                    Question(Questions[str][Random.Range(0, 3)], Choice[str]);
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
                print("fjkdlfd");
                SceneMananagementClass.SMC.LoadSceneAsSync("CheckTheAffectionScene");
                SceneMananagementClass.SMC.UnLoadSceneAsSync("CheckMemory");
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
    private int question_count = 0;
    public int QuestionCount
    {
        get { return question_count; }
        set
        {
            question_count = value;
            if (question_count < 0)
            {
                HideQ();
                State = State.END;
                // TODO : END
            }
        }
    }

    int memo_idx = 0;
    int q_idx = 0;
    int max_q_idx = 0;

    bool isA = false;

    private void Start()
    {
        State = State.GREETING;
        question_count = ListContainer.LC.PresentationResult.Count;
        max_q_idx = question_count;
    }
    private void Update()
    {
        if (IsTimer)
        {
            LeftTime += Time.deltaTime;
            if (LeftTime > 5)
            {
                // TODO : Fail
                if (!isA)
                {
                    Chatting.MessageStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text = ".....";
                    GameManager.Instance.Affection -= 10;
                    StartCoroutine(EDelay(new System.Tuple<string, bool>(Negative[Random.Range(0, 4)], false)));
                }

                IsTimer = false;
                HideQ();
                State = State.QUESTION;
            }
        }
    }

    public void NextMemo()
    {
        // TODO : ���⼭ Presentation�� �����ؼ� ���� �޸� ���ٸ� Text ���

        if (memo_idx == ListContainer.LC.PresentationResult.Count)
        {
            Content.text = "";
            return;
        }

        bool check = ListContainer.LC.PresentationResult[memo_idx].IsModifedByBadGirl;
        string visit = ListContainer.LC.PresentationResult[memo_idx].PlaceAndActionStringArr[check ? 1 : 0];
        string action = ListContainer.LC.PresentationResult[memo_idx].PlaceAndActionStringArr[check ? 3 : 2];

        Date.text = $"{System.DateTime.Now.Year} / {System.DateTime.Now.Month} / {System.DateTime.Now.Day}";

        string memo = $"{visit}���� {action}";

        switch (visit)
        {
            case "��ȭ��":
                memo += "�� �����";
                break;
            case "�������":
                memo += "�� �Ծ���";
                break;
            case "����ī��":
                memo += "�� ������";
                break;
            case "����":
                memo += "�� �� �� ���� ���ƿԴ�";
                break;
            case "������":
                memo += "�� ���� ��Ҵ�";
                break;
            case "�Ǽ��縮��":
                memo += "��� ����̸� �����ߴ�";
                break;
        }

        Content.text = memo;

        ++memo_idx;
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

            string visit = ListContainer.LC.PresentationResult[q_idx].PlaceAndActionStringArr[0];
            string action = ListContainer.LC.PresentationResult[q_idx].PlaceAndActionStringArr[2];

            if (Choice[visit][index] == action)
                StartCoroutine(EDelay(new System.Tuple<string, bool>(Positive[rand], false)));
            else
            {
                GameManager.Instance.Affection -= 10;
                StartCoroutine(EDelay(new System.Tuple<string, bool>(Negative[rand], false)));
            }

            //StartCoroutine(EDelay(new System.Tuple<string, bool>(Negative[rand], false)));


            string place = ListContainer.LC.PresentationResult[q_idx].PlaceAndActionStringArr[0];
            ++q_idx;
            Chatting.MessageStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text = GetString(place, Choice[place][index]);
            State = State.ANSWER;

            isA = true;
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
