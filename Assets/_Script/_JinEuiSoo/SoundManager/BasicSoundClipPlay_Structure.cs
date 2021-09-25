using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSoundClipPlay_Structure : MonoBehaviour
{
    [SerializeField] internal SoundPoolTag SoundPoolTag;

    [SerializeField] internal float ClipDuration;
    [SerializeField] internal float RemainTime;
    [SerializeField] internal float DelayTime;

    [SerializeField] internal AudioStorage CurrentAudioStorage;
    [SerializeField] internal AudioSource STAudioSource;
}

public enum SoundPoolTag
{
    Clip,
    Ambience,
    BGM
}
