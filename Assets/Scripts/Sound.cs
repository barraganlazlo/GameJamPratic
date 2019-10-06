﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = .8f;
    [Range(.1f, 3)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}
