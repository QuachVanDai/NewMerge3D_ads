using System;
using UnityEngine;

namespace ExampleProject.Interface
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyVelocityTargetMovable : MonoBehaviour, ITargetMovable
    {
        #region Fields

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float stopDistance = 0.1f;
        [SerializeField] Rigidbody rigid;

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
        Rigidbody Rigid => rigid != null ? rigid : (rigid = GetComponent<Rigidbody>());

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

        private void FixedUpdate()
        {
            if (!isMoving || !hasTarget)
                return;

            _currentTarget = target != null ? target.position : targetPosition;
            _distance = Vector3.Distance(transform.position, _currentTarget);

            if (_distance <= stopDistance)
            {
                transform.position = _currentTarget;
                ReachTarget();
                return;
            }

            _direction = (_currentTarget - transform.position).normalized;
            Rigid.velocity = _direction * moveSpeed;
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
            if (Rigid != null)
                Rigid.velocity = Vector3.zero;
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
                if (Rigid != null)
                    Rigid.velocity = Vector3.zero;
                OnStopMove?.Invoke();
            }
        }

        public void SetSpeed(float _speed)
        {
            moveSpeed = _speed;
        }

        public void SetStopDistance(float _distance)
        {
            stopDistance = _distance;
        }

        #endregion
    }
}
