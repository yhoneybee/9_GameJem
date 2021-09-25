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
        { "영화관", new string[] { "호러","애니메이션","레이싱","판타지" } },
        { "레스토랑", new string[] { "파스타","스테이크","스튜","빵" } },
        { "동물카페", new string[] { "고양이","개","닭","돼지" } },
        { "꽃집", new string[] { "흰색","빨간색","노란색","파란색" } },
        { "오락실", new string[] { "유령","좀비","오크","강아지 수인" } },
        { "악세서리점", new string[] { "십자가","불꽃","물방울","해골" } },
    };

    Dictionary<string, string[]> Questions = new Dictionary<string, string[]>()
    {
        { "영화관", new string[] { "영화 보러 영화관 가길 잘했어. 어제 본 장르는 잘 맞았어?","영화 고르는데 한참이더라, 그게 그렇게 좋아?","내일도 어제랑 같은 장르 영화볼래? 그 장르 좋아하는 것 같은데" } },
        { "레스토랑", new string[] { "레스토랑에서 먹은 음식은 입에 좀 맞았어?", "어제 우리 레스토랑에서 음식을 너무 많이 먹었어..", "나는 네가 그렇게 밥 많이 먹는 걸 처음 봤어." } },
        { "동물카페", new string[] { "네가 동물을 그렇게 좋아하는 줄 몰랐어.", "동물카페에서 어제 같이 쓰다듬은 동물이 뭐였지?", "너는 동물카페에서 걔 안무서웠어?" } },
        { "꽃집", new string[] { "꽃집에서 네 옷에 나비가 앉은거 너무 신기하지 않아?", "나비가 꽃집에서 널 꽃인 줄 알았나봐", "나비는 어제 왜 너한테 앉은 걸까?" } },
        { "오락실", new string[] { "어제 진짜 게임 잘하던데, 원래 괴물 안무서워 해?", "게임하다가 울어서 깜짝 놀랬잖아 ㅎㅎ. 그렇게 무서웠어?", "게임할 때 나온 괴물 꿈에서 나오는 줄 알았잖아 ㅜㅜ" } },
        { "악세서리점", new string[] { "액세서리 봤을 때 눈이 반짝거리던데 그게 취향이야?", "너가 엄청 자세히 보던 액세서리 나한테도 잘 어울릴까?", "어제 본 악세사리점에서 내 취향은 십자간데 너는 어땠어?" } },
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
            Question($"나랑 무엇을 먹었지?", new string[] { "피자", "치킨", "짜장면", "짬뽕" });
        }
    }

    public void NextMemo()
    {
        // TODO : 여기서 Presentation에 접근해서 갱신 메모가 없다면 Text 비움
        int year = 2021, month = 1, day = 1;
        Date.text = $"{year} / {month} / {day}";
        Content.text = $"{month}월 {day}일 입니다~";
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
            // TODO : Presentation에 접근해서 bool이 true라면(바뀌었다면)
            // 홀수 정답 아니라면 짝수 정답

            HideQ();
            Chatting.MessageStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text = $"대답";
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
