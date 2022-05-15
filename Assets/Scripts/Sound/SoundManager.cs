using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private List<Sound> soundList;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        #endregion

        Play(SoundType.MainTheme);

        MuteCategory(SoundCategory.Music,Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteSoundName.ToString(), 0)));
        MuteCategory(SoundCategory.SFX,Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteMusicName.ToString(), 0)));
    }

    public void MuteCategory(SoundCategory category, bool status)
    {
        soundList.FindAll(x => x.category.Equals(category)).ForEach(x => x.source.mute = status);
    }

    public void Play(SoundType type)
    {
        Sound sound = soundList.Find(x => x.type.Equals(type));

        if (!sound.source.isPlaying)
        {
            sound.source.mute = false;
            sound.source.Play();
        }
    }
}
