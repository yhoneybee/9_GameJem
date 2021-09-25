using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    private int affection;
    public int Affection
    {
        get { return affection; }
        set
        { 
            affection = value;
            if (value <= 0)
            {
                Debug.LogWarning("¾ÖÁ¤µµ was ZERO!!!!!!!!!!!!!!!!!!");
            }
        }
    }
}
