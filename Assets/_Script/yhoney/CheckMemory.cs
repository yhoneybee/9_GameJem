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
        { "영화관", new string[] { "호러","애니메이션","레이싱","판타지" } },
        { "레스토랑", new string[] { "파스타","스테이크","스튜","빵" } },
        { "동물카페", new string[] { "고양이","개","닭","돼지" } },
        { "꽃집", new string[] { "흰색","빨간색","노란색","파란색" } },
        { "오락실", new string[] { "유령","좀비","오크","강아지 수인" } },
        { "악세사리점", new string[] { "십자가","불꽃","물방울","해골" } },
    };

    Dictionary<string, string[]> Questions = new Dictionary<string, string[]>()
    {
        { "영화관", new string[] { "영화 보러 영화관 가길 잘했어. 어제 본 장르는 잘 맞았어?","영화 고르는데 한참이더라, 그게 그렇게 좋아?","내일도 어제랑 같은 장르 영화볼래? 그 장르 좋아하는 것 같은데" } },
        { "레스토랑", new string[] { "레스토랑에서 먹은 음식은 입에 좀 맞았어?", "어제 우리 레스토랑에서 음식을 너무 많이 먹었어..", "나는 네가 그렇게 밥 많이 먹는 걸 처음 봤어." } },
        { "동물카페", new string[] { "네가 동물을 그렇게 좋아하는 줄 몰랐어.", "동물카페에서 어제 같이 쓰다듬은 동물이 뭐였지?", "너는 동물카페에서 걔 안무서웠어?" } },
        { "꽃집", new string[] { "꽃집에서 네 옷에 나비가 앉은거 너무 신기하지 않아?", "나비가 꽃집에서 널 꽃인 줄 알았나봐", "나비는 어제 왜 너한테 앉은 걸까?" } },
        { "오락실", new string[] { "어제 진짜 게임 잘하던데, 원래 괴물 안무서워 해?", "게임하다가 울어서 깜짝 놀랬잖아 ㅎㅎ. 그렇게 무서웠어?", "게임할 때 나온 괴물 꿈에서 나오는 줄 알았잖아 ㅜㅜ" } },
        { "악세사리점", new string[] { "액세서리 봤을 때 눈이 반짝거리던데 그게 취향이야?", "너가 엄청 자세히 보던 액세서리 나한테도 잘 어울릴까?", "어제 본 악세사리점에서 내 취향은 십자간데 너는 어땠어?" } },
    };

    List<string[]> Greetings = new List<string[]>()
    {
        new string[] { "어제 데이트는 너무 즐거웠어", "응... 나두 너무 좋았어" },
        new string[] { "어제는 잘 들어갔어?", "덕분에 조심히 들어갔어" },
        new string[] { "어제 너무 잘 놀았다 ㅎㅎ", "맞아 ㅎㅎ" },
        new string[] { "잠은 잘 잤어?", "응, 너무 행복한 꿈을 꿨어" },
    };

    List<string> Positive = new List<string>()
    {
        "와 ㅋㅋㅋ 우리 통했네!",
        "오 너두? 나두!",
        "ㅎㅎ 나도 좋아",
        "다음에도 또 같이 가면 좋겠네!ㅎㅎ",
    };

    List<string> Negative = new List<string>()
    {
        ".........",
        "어....음..그랬나..?",
        "너 다른 사람이랑 착각하건 아냐?",
        "그거 나 아닌데?",
    };

    List<string[]> End = new List<string[]>()
    {
        new string[] { "잘자고 내일봐", "내 꿈꿔~" },
        new string[] { "다음 데이트 기대된다", "나도 기대되!" },
        new string[] { "먼저 자 ㅋㅋㅋ", "잠이 잘 안...ㅇ..ㅘ zzZ" },
        new string[] { "내 꿈꿔", "ㅇㅁㅇ ㄱ..그랭ㅎ" },
    };

    string GetString(string key, string select)
    {
        string result = "";
        switch (key)
        {
            case "영화관":
                result = $"영화관에서 본 {select} 장르 영화는 너무 재밌고 즐거웠어!";
                break;
            case "레스토랑":
                result = $"그 레스토랑 {select} 정말 맛있었어.. 다시 가고 싶어!";
                break;
            case "동물카페":
                result = $"{select}는 너무 귀여웠어!";
                break;
            case "꽃집":
                result = $"{select} 나비가 날 좋아하나봐";
                break;
            case "오락실":
                result = $"{select}는 많이 무서웠어 ㅠ";
                break;
            case "악세사리점":
                result = $"{select}모양은 내 취향이었어";
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
        // TODO : 여기서 Presentation에 접근해서 갱신 메모가 없다면 Text 비움

        if (memo_idx == ListContainer.LC.PresentationResult.Count)
        {
            Content.text = "";
            return;
        }

        bool check = ListContainer.LC.PresentationResult[memo_idx].IsModifedByBadGirl;
        string visit = ListContainer.LC.PresentationResult[memo_idx].PlaceAndActionStringArr[check ? 1 : 0];
        string action = ListContainer.LC.PresentationResult[memo_idx].PlaceAndActionStringArr[check ? 3 : 2];

        Date.text = $"{System.DateTime.Now.Year} / {System.DateTime.Now.Month} / {System.DateTime.Now.Day}";

        string memo = $"{visit}에서 {action}";

        switch (visit)
        {
            case "영화관":
                memo += "를 골랐다";
                break;
            case "레스토랑":
                memo += "를 먹었다";
                break;
            case "동물카페":
                memo += "를 만졌다";
                break;
            case "꽃집":
                memo += "옷 위 로 나비가 날아왔다";
                break;
            case "오락실":
                memo += "를 때려 잡았다";
                break;
            case "악세사리점":
                memo += "모양 목걸이를 구경했다";
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
            // TODO : Presentation에 접근해서 bool이 true라면(바뀌었다면)
            // 홀수 정답 아니라면 짝수 정답

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
