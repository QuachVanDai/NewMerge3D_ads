using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using ExampleProject.Tools;

namespace ExampleProject.UI.Shared
{
    public class FloatingText : MonoBehaviour
    {
        #region Fields

        [SerializeField] RectTransform rectTransform;
        [SerializeField] Text damageText;
        [SerializeField] float jumpPower = 45f;                 // arc height
        [SerializeField] float fallDistance = 30f;              // landing below start
        [SerializeField] float horizontalDistanceMin = 20f;     // random side drift min
        [SerializeField] float horizontalDistanceMax = 45f;     // random side drift max
        [SerializeField] float duration = 0.8f;
        [SerializeField] Ease jumpEase = Ease.Linear;
        [SerializeField] CanvasGroup canvasGroup;

        Sequence sequence;

        #endregion

        #region Properties


        #endregion

        #region LifeCycle

        private void OnDisable()
        {
            // Kill any tweens or delayed calls that were created for this instance (by id = this GameObject)
            DOTween.Kill(gameObject, false);
            sequence?.Kill();
            sequence = null;
        }
        void OnDestroy()
        {
            DOTween.Kill(gameObject, false);
            sequence?.Kill();
            sequence = null;
        }

        #endregion

        #region Public Methods

        public void Show(string _text)
        {
            DOTween.Kill(gameObject, false);
            sequence?.Kill();
            sequence = null;

            damageText.text = _text;

            // Reset alpha in case object is reused
            canvasGroup.alpha = 1f;

            Vector2 _startPos = rectTransform.anchoredPosition;

            float _sideSign = Random.value < 0.5f ? -1f : 1f;
            float _xOffset = Random.Range(horizontalDistanceMin, horizontalDistanceMax) * _sideSign;

            Vector2 _endPos = new(
                _startPos.x + _xOffset,
                _startPos.y - fallDistance
            );

            // Create a recyclable sequence and set a target/id so we can kill everything for this instance at once
            sequence = DOTween.Sequence()
                .SetRecyclable(true)
                .SetAutoKill(true)
                .SetTarget(gameObject);

            // Parabolic jump: up then down, with random left/right movement
            sequence.Append(rectTransform.DOJumpAnchorPos(_endPos, jumpPower, 1, duration).SetEase(jumpEase));

            // Fade out during second half
            sequence.Join(canvasGroup.DOFade(0f, duration * 0.5f).SetDelay(duration * 0.5f));

            // Ensure sequence uses independent updates if you need it (unchanged from before)
            sequence.SetUpdate(true);

            // On normal completion recycle if still spawned
            sequence.OnComplete(() =>
            {
                if (ObjectPool.IsSpawned(gameObject))
                    ObjectPool.Recycle(gameObject);
            });

            // // Safety fallback: if for some reason OnComplete never runs, recycle after duration + small buffer.
            // // Use same id (this GameObject) so the fallback is killed when the object is disabled/recycled.
            // DOVirtual.DelayedCall(duration + 0.25f, () =>
            // {
            //     if (ObjectPool.IsSpawned(gameObject))
            //         ObjectPool.Recycle(gameObject);
            // }).SetId(gameObject).SetUpdate(true);
        }

        public void SetAnchorPos(Vector2 _pos)
        {
            rectTransform.anchoredPosition = _pos;
        }
        public void ResetLocalScale()
        {
            rectTransform.localScale = Vector3.one;
        }

        #endregion
    }
}