using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ExampleProject.Gameplay.Faction;
using System.Collections.Generic;

namespace ExampleProject.UI.Shared
{
    public class HealthBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] FactionId faction;
        [SerializeField] List<FactionData> FactionDatas;
        [SerializeField] Slider slider;
        [SerializeField] Image fillImage;
        [SerializeField] Image backgroundImage;
        [SerializeField] Image ghostImage;

        [SerializeField] float animationDuration = 0.3f;
        [SerializeField] Ease animationEase = Ease.OutCubic;

        float maxHealth = 100f;
        float currentHealth = 100f;
        Sequence healthTween;

        #endregion

        #region Properties

        public float MaxHealth => maxHealth;

        public float CurrentHealth => currentHealth;

        #endregion

        #region LifeCycle           

        private void OnDisable()
        {
            healthTween?.Kill();
        }

        #endregion

        #region Private Methods

        void SetHealth(float _healthPercent, bool _animate = true)
        {
            _healthPercent = Mathf.Clamp(_healthPercent, 0f, maxHealth);
            currentHealth = _healthPercent;

            healthTween?.Kill();

            if (_animate)
            {
                healthTween = DOTween.Sequence();
                healthTween.Append(
                    slider.DOValue(currentHealth, animationDuration)
                    .SetEase(animationEase)
                    );
                healthTween.Append(
                    ghostImage.DOFillAmount(currentHealth / maxHealth, animationDuration)
                    .SetEase(animationEase).SetDelay(0.2f)
                    );
                healthTween.Play();
            }
            else
            {
                slider.value = currentHealth;
            }
        }

        #endregion

        #region Public Methods

        public void SetActive(bool _active)
        {
            gameObject.SetActive(_active);
        }
        public void SetHealthPercent(float _percent, bool _animate = true)
        {
            SetHealth(_percent, _animate);
        }
        public void Initialize(FactionId _faction)
        {
            faction = _faction;
            fillImage.sprite = GetResourceData(faction).healthBarForeground;
            backgroundImage.sprite = GetResourceData(faction).healthBarBackground;

            // Default to 100 percent max health
            maxHealth = 100;
            slider.maxValue = maxHealth;
            SetHealth(100, false);
        }
        public FactionData GetResourceData(FactionId _id)
        {
            var _data = FactionDatas.Find(x => x.id.Equals(_id));
            return _data;
        }
        #endregion
    }
}