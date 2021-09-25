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

    public int affection = 100;
}
