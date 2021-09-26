using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typing : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMesh;
    [Multiline]
    [SerializeField] string _text;
    [SerializeField] float _timeForShowText = 0.05f;

    private void Start()
    {
        StartCoroutine(ETMPTyping(_textMesh, _text, _timeForShowText));
    }

    IEnumerator ETMPTyping(TextMeshPro text, string str, float time = 0.01f)
    {
        for (int i = 0; i < str.Length; i++)
        {
            text.text = str.Substring(0, i);
            yield return new WaitForSeconds(time);
        }

        yield return null;
    }

    IEnumerator ETyping(Text text, string str, float time = 0.01f)
    {
        for (int i = 0; i < str.Length; i++)
        {
            text.text = str.Substring(0, i);
            yield return new WaitForSeconds(time);
        }

        yield return null;
    }
}
