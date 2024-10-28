using System.Collections.Generic;
using Scripts.Constant;
using Scripts.Logic;
using Scripts.Pause;
using Scripts.Sound;
using Scripts.UI;
using UnityEngine;

namespace Scripts.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private const int DefaultLevel = 1;

        [SerializeField] private MixerChannelChanger _music;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private UserInterfaceController _userInterfaceController;
        [SerializeField] private GameLogic _gameLogic;
        [SerializeField] private List<PauseSource> _pauseSources;

        private void Awake()
        {
            int currentLevel;

            if (PlayerPrefs.HasKey(PlayerPrefNames.Level))
            {
                currentLevel = PlayerPrefs.GetInt(PlayerPrefNames.Level);
            }
            else
            {
                currentLevel = DefaultLevel;
                PlayerPrefs.SetInt(PlayerPrefNames.Level, DefaultLevel);
            }

            _gameLogic.Init(currentLevel);
            _userInterfaceController.Init(_gameLogic);
            _pauseController.Init(_pauseSources);
        }

        private void Start() =>
            _music.Init(PlayerPrefNames.MusicMixerChannel);
    }
}