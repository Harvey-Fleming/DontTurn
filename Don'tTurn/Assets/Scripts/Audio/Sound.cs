using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { soundEffect, music}
    public AudioTypes audiotype;

    [HideInInspector] public AudioSource source;
    public AudioClip audioClip;
    public string clipName;
    public bool isLoop;

    [Range(0,1)]
    public float volume = 0.5f;
}
