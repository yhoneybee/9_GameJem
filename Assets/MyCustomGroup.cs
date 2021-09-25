using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyCustomGroup : MonoBehaviour
{
    public List<RectTransform> Rects = new List<RectTransform>();
    [SerializeField] float start_y;
    [SerializeField] float spacing_y;

    public void AddChild(RectTransform rt)
    {
        rt.SetParent(transform, false);
        Rects.Add(rt);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -(rt.sizeDelta.y + spacing_y) * (transform.childCount - 1));
        var RT = GetComponent<RectTransform>();
        RT.sizeDelta = new Vector2(RT.sizeDelta.x, transform.childCount * (rt.sizeDelta.y + spacing_y));
    }
}
