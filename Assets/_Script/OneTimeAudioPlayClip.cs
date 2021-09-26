using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAudioPlayClip : MonoBehaviour
{
    [SerializeField] string _clipName;

    void Start()
    {
        SoundManager.SM.RequestPlayClip(_clipName);
    }

}
