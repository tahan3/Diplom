using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public enum SoundCategory : byte
{
    Music,
    SFX
}

[Serializable]
public enum SoundType : byte
{
    MainTheme
}

[Serializable]
public class Sound
{
    public SoundCategory category;
    public SoundType type;
    public float volume;
    public AudioClip clip;
    public AudioSource source;
}