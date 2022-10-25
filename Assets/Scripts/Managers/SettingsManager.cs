using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Image musicToggle;
        [SerializeField] private Image soundToggle;
        [SerializeField] private Image vibroToggle;
    
        [Header("Sprites")]
        [SerializeField] private Sprite toggleOn;
        [SerializeField] private Sprite toggleOff;

        private bool currentMusicStatus;
        private bool currentSoundStatus;
        private bool currentVibroStatus;

        private void Awake()
        {
            currentMusicStatus = Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteMusicName.ToString(), 0));
            currentSoundStatus = Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteSoundName.ToString(), 0));
            currentVibroStatus = Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteVibroName.ToString(), 0));

            SetToggle(currentMusicStatus, musicToggle);
            SetToggle(currentSoundStatus, soundToggle);
            SetToggle(VibroManager.status, vibroToggle);
        }

        public void MusicMute()
        {
            currentMusicStatus = !currentMusicStatus;
            SoundManager.Instance.MuteCategory(SoundCategory.Music, currentSoundStatus);
            SetToggle(currentMusicStatus, musicToggle);
        }

        public void SoundsMute()
        {
            currentSoundStatus = !currentSoundStatus;
            SoundManager.Instance.MuteCategory(SoundCategory.SFX, currentSoundStatus);
            SetToggle(currentSoundStatus, soundToggle);
        }

        public void VibrateMute()
        {
            currentVibroStatus = !currentVibroStatus;
            SetToggle(currentVibroStatus, vibroToggle);
        }

        private void SetToggle(bool status, Image image)
        {
            image.sprite = status ? toggleOff : toggleOn;
        }
    
        private void OnDisable()
        {
            PlayerPrefs.SetInt(PREFS_NAMES.MuteVibroName.ToString(), currentVibroStatus ? 1 : 0);
            PlayerPrefs.SetInt(PREFS_NAMES.MuteMusicName.ToString(), currentMusicStatus ? 1 : 0);
            PlayerPrefs.SetInt(PREFS_NAMES.MuteSoundName.ToString(), currentSoundStatus ? 1 : 0);

            VibroManager.status = currentVibroStatus;
        }
    }
}