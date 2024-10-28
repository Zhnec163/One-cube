using System.Collections;
using DG.Tweening;
using Scripts.Logic;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private GameObject _label;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Vector3 _punchScale;
        [SerializeField] private float _punchDuration;
        [SerializeField] private float _showTime;

        private Coroutine _closing;
        private WaitForSeconds _delay;

        private void Awake()
        {
            _delay = new WaitForSeconds(_showTime);
            _score.ComboChanging += OnComboChanging;
        }

        private void OnDestroy() =>
            _score.ComboChanging -= OnComboChanging;

        private void OnComboChanging(int combo)
        {
            if (combo > 1)
            {
                _text.text = combo.ToString();
                _text.gameObject.SetActive(true);
                _label.SetActive(true);
                transform.DOPunchScale(_punchScale, _punchDuration);

                if (_closing != null)
                    StopCoroutine(_closing);

                _closing = StartCoroutine(Closing());
            }
        }

        private IEnumerator Closing()
        {
            yield return _delay;
            _label.SetActive(false);
            _text.gameObject.SetActive(false);
        }
    }
}