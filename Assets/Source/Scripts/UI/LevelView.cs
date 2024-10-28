using Scripts.Logic;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameLogic _gameLogic;

        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _gameLogic.LevelChanged += OnLevelChanged;
            UpdateText(_gameLogic.CurrentLevel.ToString());
        }

        private void OnDestroy() =>
            _gameLogic.LevelChanged -= OnLevelChanged;

        private void OnLevelChanged(int level) =>
            UpdateText(level.ToString());

        private void UpdateText(string text) =>
            _text.text = text;
    }
}