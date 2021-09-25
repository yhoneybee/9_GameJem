using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WidthScaleSetFromStringLength : MonoBehaviour
{
    [SerializeField] RectTransform RT;
    [SerializeField] TextMeshProUGUI String;

    private void Update()
    {
        RT.sizeDelta = new Vector2(Mathf.Min(900, Mathf.Max((600 * String.text.Length) / 20, 600)), RT.sizeDelta.y);
    }
}
