using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chatting : MonoBehaviour
{
    public RectTransform Content;
    [SerializeField] Scrollbar Scrollbar;
    [SerializeField] GameObject ChatPrefab;
    [SerializeField] Sprite Me;
    [SerializeField] Sprite You;

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
        var obj = Instantiate(ChatPrefab, Content);
        var img = obj.GetComponent<Image>();
        var text = obj.GetComponentInChildren<TextMeshProUGUI>();

        obj.transform.SetParent(Content.transform, false);
        text.text = message;

        //if (send) img.sprite = Me;
        //else img.sprite = You;

        SoundManager.SM.RequestPlayClip("Ä«Åå¼Ò¸®");

        if (send)
        {
            //img.color = Color.yellow;
            img.sprite = Me;
            text.alignment = TextAlignmentOptions.Right;
        }
        else
        {
            //img.color = Color.white;
            img.sprite = You;
            text.alignment = TextAlignmentOptions.Left;
        }

        SetZero();

        var glg = Content.GetComponent<GridLayoutGroup>();
        Content.sizeDelta = new Vector2(Content.sizeDelta.x, (glg.cellSize.y + glg.spacing.y) * Content.childCount + 25);

        if (send) MessageStack.Push(obj);
    }

    void InvokeZero() => Scrollbar.value = 0;

    public void SetZero()
    {
        Invoke(nameof(InvokeZero), 0.1f);
    }
}
