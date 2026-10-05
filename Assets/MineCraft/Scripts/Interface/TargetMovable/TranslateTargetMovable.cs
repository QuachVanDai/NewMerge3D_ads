using System;
using UnityEngine;

namespace ExampleProject.Interface
{
    public class TranslateTargetMovable : MonoBehaviour, ITargetMovable
    {
        #region Fields

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float stopDistance = 0.1f;

        Transform target;
        Vector3 targetPosition;
        bool isMoving;
        bool hasTarget;

        #endregion

        #region Properties

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public bool IsMoving => isMoving;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        #endregion

        #region Events

        public event Action OnStartMove;
        public event Action OnStopMove;
        public event Action OnUpdateMove;
        public event Action OnTargetReached;

        #endregion

        #region LifeCycle   

        Vector3 _currentTarget;
        Vector3 _direction;
        float _distance;
        float _step;
        private void Update()
        {
            if (!isMoving || !hasTarget)
                return;

            _currentTarget = target != null ? target.position : targetPosition;
            _direction = (_currentTarget - transform.position).normalized;
            _distance = Vector3.Distance(transform.position, _currentTarget);

            // Calculate the step for this frame and avoid overshooting the target.
            _step = moveSpeed * Time.deltaTime;

            if (_distance <= stopDistance || _step >= _distance)
            {
                // Snap exactly to the target and trigger reached.
                transform.position = _currentTarget;
                ReachTarget();
                return;
            }

            transform.Translate(_step * _direction, Space.World);
            OnUpdateMove?.Invoke();
        }

        #endregion

        #region Private Methods

        private void StartMoving()
        {
            if (!isMoving)
            {
                isMoving = true;
                hasTarget = true;
                OnStartMove?.Invoke();
            }
        }

        private void ReachTarget()
        {
            isMoving = false;
            hasTarget = false;
            OnTargetReached?.Invoke();
        }

        #endregion

        #region Public Methods

        public void MoveTo(Transform _target)
        {
            if (_target == null)
            {
                Debug.LogWarning("Target transform is null!");
                StopMove();
                return;
            }

            target = _target;
            StartMoving();
        }
        public void MoveTo(Vector3 _targetPosition)
        {
            target = null;
            targetPosition = _targetPosition;
            StartMoving();
        }
        public void StopMove()
        {
            if (isMoving)
            {
                isMoving = false;
                hasTarget = false;
                OnStopMove?.Invoke();
            }
        }
        public void SetSpeed(float _speed)
        {
            moveSpeed = _speed;
        }
        public void SetStopDistance(float _dis)
        {
            stopDistance = _dis;
        }

        #endregion
    }
}
