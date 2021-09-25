using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicSoundClipPlay_Structure))]
public class BasicSoundClipPlay_Common : MonoBehaviour
{
    BasicSoundClipPlay_Structure _zz;
    BasicSoundClipPlay_Structure ST
    {
        get
        {
            if (_zz == null)
                _zz = this.gameObject.GetComponent<BasicSoundClipPlay_Structure>();

            return _zz;
        }
    }
    void Initialization()
    {
        ST.ClipDuration = ST.CurrentAudioStorage.audioClip.length;
        ST.RemainTime = ST.DelayTime + ST.ClipDuration;
        this.name = ST.CurrentAudioStorage.name;
        ST.STAudioSource.clip = ST.CurrentAudioStorage.audioClip;
        ST.STAudioSource.Play();
    }

    private void Update()
    {
        ST.RemainTime -= Time.deltaTime;
        if(ST.RemainTime < 0f)
        {
            ST.CurrentAudioStorage = null;
            SoundManager.SM.ClipRequestReturnToPool(this.transform);
        }
    }

    internal void SetSoundPoolTag(SoundPoolTag poolTag) => ST.SoundPoolTag = poolTag;

    internal void SetDelayTime(float value) => ST.DelayTime = value;

    internal void SetInitialization(AudioStorage audioStorage)
    {
        ST.CurrentAudioStorage = audioStorage;
        Initialization();
    }
}
