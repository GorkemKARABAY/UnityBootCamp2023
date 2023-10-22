using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class UISettings : MonoBehaviour
    {

        private GameManager _gameManager;
        public Toggle MuteMusicToggle;
        public Toggle MuteSoundToggle;

        public Slider MusicVolumeSlider;
        public Slider SfxVolumeSlider;

        public GameObject OptionsPanel;
        public GameObject LevelCompletedPanel;


        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;            

            if (_gameManager.AudioPlayer.IsMusicMuted)
                MuteMusicToggle.isOn = true;
            else
                MuteMusicToggle.isOn = false;

            if (_gameManager.AudioPlayer.IsSoundMuted)
                MuteSoundToggle.isOn = true;
            else
                MuteSoundToggle.isOn = false;

            MusicVolumeSlider.value = _gameManager.AudioPlayer.MusicVolume;
            SfxVolumeSlider.value = _gameManager.AudioPlayer.SoundVolume;

            MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
            SfxVolumeSlider.onValueChanged.AddListener(delegate { SoundVolumeChange(); });

            MuteMusicToggle.onValueChanged.AddListener(delegate { MuteMusic(); });
            MuteSoundToggle.onValueChanged.AddListener(delegate { MuteSound(); });

            OptionsPanel.SetActive(false);
            LevelCompletedPanel.SetActive(false);

            _gameManager.OnLevelCompleted += OpenLevelFinishPanel;
        }

        private void OnDestroy()
        {
            _gameManager.OnLevelCompleted -= OpenLevelFinishPanel;
        }

        private void OpenLevelFinishPanel()
        {
            LevelCompletedPanel.SetActive(true);
        }


        private void MuteSound()
        {
            _gameManager.AudioPlayer.IsSoundMuted = MuteSoundToggle.isOn;
        }

        private void MuteMusic()
        {
            _gameManager.AudioPlayer.IsMusicMuted = MuteMusicToggle.isOn;
        }

        public void MusicVolumeChange()
        {
            _gameManager.AudioPlayer.MusicVolume = MusicVolumeSlider.value;
        }

        public void SoundVolumeChange()
        {
            _gameManager.AudioPlayer.SoundVolume = SfxVolumeSlider.value; 
        }

        public void GotoNextLevel()
        {
            _gameManager.GotoNextLevel();
            LevelCompletedPanel.SetActive(false);
        }

    }
}