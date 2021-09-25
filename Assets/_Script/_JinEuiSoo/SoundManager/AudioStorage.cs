using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClipStorage", menuName = "AuidoClipStorage")]
public class AudioStorage : ScriptableObject
{
    [SerializeField] string _name;
    public new string name => _name;

    [SerializeField] AudioClip _audioClip;
    public AudioClip audioClip => _audioClip;

}
