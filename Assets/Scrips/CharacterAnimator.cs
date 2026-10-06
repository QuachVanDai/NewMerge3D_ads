using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


namespace ExampleProject.Gameplay.Characters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected AnimationClip attackAnim;
        [SerializeField] protected AnimationClip deathAnim;
        [SerializeField] protected AnimationClip runAnim;
        [SerializeField] protected AnimationClip idleAnim;
        [SerializeField] protected AnimationClip pickAnim;
        [SerializeField] protected AnimationClip jumpAnim;
        [SerializeField] protected List<AnimationClip> victoryAnim;
        [SerializeField] protected AnimationClip spawnAnim;
        [SerializeField] protected List<AnimationClip> defeatAnim;

        Animator animator;
        public Action onAttackPointReached;
        public Action<AnimationEvent> onAttacStart;
        public Action<AnimationEvent> onJump;
        public Action<AnimationEvent> onLand;
        public Action<AnimationEvent> onFootStep;

        #endregion

        #region Properties

        Animator Animator => animator != null ? animator : (animator = GetComponent<Animator>());

        #endregion

        #region LifeCycle   



        #endregion

        #region Private Methods

        protected IEnumerable<AnimationClip> GetAllAnimationClips()
        {
            return GetAnimationClip();
        }

        #endregion

        #region Public Methods

        public void Play(string _animationName, Action _onComplete = null)
        {
            Animator.Play(_animationName);
            if (_onComplete != null)
            {
                StartCoroutine(WaitForAnimation(_animationName, _onComplete));
            }
        }
        IEnumerator WaitForAnimation(string _animationName, Action _onComplete)
        {
            // Wait until the animation state is playing
            AnimatorStateInfo _stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            while (!_stateInfo.IsName(_animationName))
            {
                yield return null;
                _stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            }

            // Wait until the animation finishes
            while (_stateInfo.normalizedTime < 1.0f)
            {
                yield return null;
                _stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            }

            _onComplete?.Invoke();
        }
        public void ResetTrigger(string animationName)
        {
            Animator.ResetTrigger(animationName);
        }
        public void CrossFade(string _animationName, float _fadeLength = 0.1f)
        {
            AnimatorStateInfo _currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            if (_currentStateInfo.IsName(_animationName))
            {
                // Restart without crossfade (direct reset)
                Animator.Play(_animationName, 0, 0);
            }
            else
            {
                // Crossfade to different animation
                // Animator.CrossFade(_animationName, _fadeLength);
                Animator.Play(_animationName, 0, 0);
            }
        }
        public void SetSpeed(float _speed)
        {
            Animator.speed = _speed;
            //Animator.SetFloat("AttackSpeed", _speed);
        }
        public void SetWeaponRunAnim(bool _isHaveWeapon)
        {
            //Animator.speed = _speed;
            float _value = _isHaveWeapon ? 1f : 0f;
            Animator.SetFloat("IsHaveWeapon", _value);
        }
        public AnimationClip GetRandomAnimationClip()
        {
            var _clips = GetAnimationClip().ToList();
            if (_clips == null || _clips.Count == 0)
                return null;

            int index = Random.Range(0, _clips.Count);
            return _clips[index];
        }
        public float GetAnimatorProgress()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        }
        public void PlayAnimationAtTime(string _animationName, float _normalizedTime)
        {
            if (Animator == null)
                return;

            float _t = Mathf.Clamp01(_normalizedTime);

            Animator.Play(_animationName, 0, _t);
            Animator.Update(0f);
        }
        public IEnumerable<AnimationClip> GetAnimationClip()
        {
            if (Animator == null || Animator.runtimeAnimatorController == null)
                return new List<AnimationClip>();

            return Animator.runtimeAnimatorController.animationClips
                .Select(clip => clip)
                .Distinct()
                .ToList();
        }

        void OnAttackStart(AnimationEvent animationEvent)
        {
            onAttacStart?.Invoke(animationEvent);
        }
        public void OnAttackPointReached()
        {
            onAttackPointReached?.Invoke();
        }
        public void OnFootStepReached(AnimationEvent animationEvent)
        {
            onFootStep?.Invoke(animationEvent);
        }
        public void OnJump(AnimationEvent animationEvent)
        {
            onJump?.Invoke(animationEvent);
        }
        public void OnLand(AnimationEvent animationEvent)
        {
            onLand?.Invoke(animationEvent);
        }

        internal void SetAnimatorOverrideController(AnimatorOverrideController _animatorOverride)
        {
            // Assign the new override controller back to the Animator component
            Animator.runtimeAnimatorController = _animatorOverride;
        }
        public float GetAnimationLength(string _originalAnimName)
        {
            AnimatorOverrideController _aoc = Animator.runtimeAnimatorController as AnimatorOverrideController;
            if (_aoc == null)
                return 0f;

            var _overrideAnim = _aoc[_originalAnimName];
            return _overrideAnim != null ? _overrideAnim.length : 0f;
        }

        public void RandomDance()
        {
            AnimationClip _randomeDance = victoryAnim[Random.Range(0, victoryAnim.Count)];
            CrossFade(_randomeDance.name);
            SetSpeed(1);
        }
        public void Death()
        {
            CrossFade(deathAnim.name);
            SetSpeed(1);
        }
        public void Run()
        {
            CrossFade(runAnim.name);
            SetSpeed(1);
        }
        public void Attack(float _interval)
        {
            float _speed = GetAnimationLength(attackAnim.name) / _interval;
            CrossFade(attackAnim.name);
            SetSpeed(_speed);
        }
        public void Idle()
        {
            CrossFade(idleAnim.name);
            SetSpeed(1);
        }
        public void PickUp()
        {
            CrossFade(pickAnim.name);
            SetSpeed(1);
        }
        public void Spawn()
        {
            Play(spawnAnim.name, () =>
            {
                bool _isPlayintSpawnAnim = Animator.GetCurrentAnimatorStateInfo(0).IsName(spawnAnim.name);
                if (_isPlayintSpawnAnim)
                {
                    CrossFade(idleAnim.name);
                    SetSpeed(1);
                }
            });
        }
        public void Jump(float _speed = 1)
        {
            CrossFade(jumpAnim.name);
            SetSpeed(_speed);
        }
        public void RandomDefeat()
        {
            AnimationClip _randomDefeat = defeatAnim[Random.Range(0, defeatAnim.Count)];
            CrossFade(_randomDefeat.name);
            SetSpeed(1);
        }
        #endregion
    }
}