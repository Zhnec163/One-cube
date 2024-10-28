using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CubeMover))]
    public class Cube : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float _upscaleDuration;

        private CubeMover _cubeMover;

        public bool IsActivated { get; private set; }

        public event Action ActivationChanged;

        private void Awake()
        {
            _cubeMover = GetComponent<CubeMover>();
            _cubeMover.MoveEnded += OnMoveEnded;
            Upscaling();
        }

        private void OnDestroy() =>
            _cubeMover.MoveEnded -= OnMoveEnded;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_cubeMover.TryMove() == false)
                return;

            IsActivated = true;
            ActivationChanged?.Invoke();
        }

        private void OnMoveEnded() =>
            gameObject.SetActive(false);

        private void Upscaling()
        {
            Vector3 defaultScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(defaultScale, _upscaleDuration);
        }
    }
}