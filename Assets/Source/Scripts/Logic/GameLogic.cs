using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Constant;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Logic
{
    public class GameLogic : MonoBehaviour
    {
        private const int MaxLevel = 30;

        [SerializeField] private PatternBuilder _patternBuilder;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Score _score;

        private List<Cube> _cubes;
        private bool _isGameActive;

        public event Action<int> LevelChanged;
        public event Action<int> LevelEnded;
        public event Action GameEnded;

        public int CurrentLevel { get; private set; }

        public void Init(int currentLevel)
        {
            _isGameActive = true;
            CurrentLevel = currentLevel;
            _nextLevelButton.onClick.AddListener(OnClickNextLevelButton);
            BuildLevel();
        }

        private void OnDestroy() =>
            _nextLevelButton.onClick.RemoveListener(OnClickNextLevelButton);

        private void Update()
        {
            if (_isGameActive == false)
                return;

            if (HaveCubes())
                return;

            if (CurrentLevel == MaxLevel)
            {
                GameEnded?.Invoke();
            }
            else
            {
                RecordScore();
                LevelEnded?.Invoke(_score.Points);
                _score.ResetPoints();
            }

            _isGameActive = false;
        }

        private void OnClickNextLevelButton()
        {
            ClearLevel();
            YandexGame.FullscreenShow();
            BuildNextLevel();
        }

        private void ClearLevel()
        {
            _cubes.ForEach(cube => cube.ActivationChanged -= OnActivationChanged);
            _cubes.ForEach(cube => Destroy(cube.gameObject));
        }

        private bool HaveCubes() =>
            _cubes.Any(cube => cube.IsActivated == false);

        private void BuildNextLevel()
        {
            CurrentLevel++;
            LevelChanged?.Invoke(CurrentLevel);
            PlayerPrefs.SetInt(PlayerPrefNames.Level, CurrentLevel);
            BuildLevel();
        }

        private void BuildLevel()
        {
            _isGameActive = true;
            _cubes = _patternBuilder.Build(CurrentLevel);
            _cubes.ForEach(cube => cube.ActivationChanged += OnActivationChanged);
        }

        private void OnActivationChanged() =>
            _score.IncrementPoints();

        private void RecordScore()
        {
            if (PlayerPrefs.HasKey(PlayerPrefNames.BestScore))
            {
                int bestScore = PlayerPrefs.GetInt(PlayerPrefNames.BestScore);

                if (bestScore < _score.Points)
                    YandexGame.NewLeaderboardScores(PlayerPrefNames.TableName, _score.Points);
            }
        }
    }
}