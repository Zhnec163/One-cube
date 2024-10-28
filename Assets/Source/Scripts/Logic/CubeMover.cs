using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Logic
{
    public class CubeMover : MonoBehaviour
    {
        private const float FailStepDistance = 1F;
        private const float RayDistance = 2F;

        [SerializeField] private Transform _raycastPoint;
        [SerializeField] private Vector3 _punchDirection;
        [SerializeField] private float _punchDuration;
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _failStepDuration;

        private bool _isMoving;
        private Tween _punch;

        public event Action MoveEnded;

        public bool TryMove()
        {
            if (_isMoving)
                return false;

            if (Physics.Raycast(_raycastPoint.position, transform.forward, out RaycastHit hit, RayDistance))
            {
                if (hit.distance > FailStepDistance)
                    PlayFailStep();
                else
                    _ = TryPunch();

                return false;
            }

            Move();
            return true;
        }

        private void Move()
        {
            _isMoving = true;
            Vector3 endValue = transform.position + transform.forward * _moveDistance;
            transform.DOMove(endValue, _moveDuration).OnComplete(() => MoveEnded?.Invoke());
        }

        private void PlayFailStep()
        {
            _isMoving = true;
            Vector3 endValue = transform.position + transform.forward;
            int loops = 2;
            transform.DOMove(endValue, _failStepDuration).SetLoops(loops, LoopType.Yoyo).OnComplete(ResetPosition);
        }

        private bool TryPunch()
        {
            if (_punch is { active: true })
                return false;

            _punch = transform.DOPunchPosition(_punchDirection, _punchDuration);
            return true;
        }

        private void ResetPosition() =>
            _isMoving = false;
    }
}