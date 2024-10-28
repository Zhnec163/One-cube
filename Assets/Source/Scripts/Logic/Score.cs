using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Logic
{
    public class Score : MonoBehaviour
    {
        private const int DefaultCombo = 1;

        [SerializeField] private int _coefficient;
        [SerializeField] private float _comboTime;

        private int _currentCombo;
        private Coroutine _comboResetting;
        private WaitForSeconds _delay;

        public event Action<int> ComboChanging;

        public int Points { get; private set; }

        private void Awake()
        {
            _currentCombo = DefaultCombo;
            _delay = new WaitForSeconds(_comboTime);
        }

        public void ResetPoints() =>
            Points = 0;

        public void IncrementPoints()
        {
            Points += _coefficient * _currentCombo;
            ComboChanging?.Invoke(_currentCombo);
            _currentCombo++;

            if (_comboResetting != null)
                StopCoroutine(_comboResetting);

            _comboResetting = StartCoroutine(ComboResetting());
        }

        public void AddCoefficient(int coefficient) =>
            _coefficient += coefficient;

        private IEnumerator ComboResetting()
        {
            yield return _delay;
            _currentCombo = DefaultCombo;
        }
    }
}