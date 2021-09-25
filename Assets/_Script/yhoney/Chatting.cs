using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chatting : MonoBehaviour
{
    public RectTransform Content;
    [SerializeField] Scrollbar Scrollbar;
    [SerializeField] GameObject YouChatPrefab;
    [SerializeField] GameObject MeChatPrefab;

    Coroutine CSetValueLerp;

    public Stack<GameObject> MessageStack = new Stack<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Chat("text", true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Chat("TEXT", false);
        }
    }

    public void Chat(string message, bool send)
    {
        GameObject obj = null;

        if (send)
        {
            obj = Instantiate(MeChatPrefab, Content);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 0);
        }
        else
        {
            obj = Instantiate(YouChatPrefab, Content);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(20, 0);
        }

        var text = obj.GetComponentInChildren<TextMeshProUGUI>();

        Content.GetComponent<MyCustomGroup>().AddChild(obj.GetComponent<RectTransform>());
        text.text = message;

        SoundManager.SM.RequestPlayClip("Ä«Åå¼Ò¸®");

        SetZero();

        //var glg = Content.GetComponent<GridLayoutGroup>();
        //Content.sizeDelta = new Vector2(Content.sizeDelta.x, (glg.cellSize.y + glg.spacing.y) * Content.childCount + 25);

        if (send) MessageStack.Push(obj);
    }

    void InvokeZero() => Scrollbar.value = 0;

    public void SetZero()
    {
        Invoke(nameof(InvokeZero), 0.1f);
    }
}
