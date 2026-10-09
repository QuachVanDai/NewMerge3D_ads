
using DG.Tweening;
using UnityEngine;

public class BossIncomingAnimation : Singleton<BossIncomingAnimation>
{
    [Header("References")]
    [SerializeField] private CanvasGroup darkOverlay;
    [SerializeField] private RectTransform bannerLeft;
    [SerializeField] private RectTransform bannerRight;
    [SerializeField] private RectTransform bossIcon;
    [SerializeField] private RectTransform bossText;

    [Header("Warning Stripes")]
    [SerializeField] private RectTransform topStripe;
    [SerializeField] private RectTransform bottomStripe;
    [SerializeField] private float stripeMoveDistance = 100f;
    [SerializeField] private float stripeDuration = 0.4f;

    [Header("Settings")]
    [SerializeField] private float bannerDistance = 800f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float holdTime = 0.7f;
    [SerializeField] private float darkAlpha = 0.35f;
    [SerializeField] private UISpawner UISpawnerArrowRight;
    [SerializeField] private UISpawner UISpawnerArrowLeft;
    [SerializeField] private UISpawner UISpawnerTopStripe;
    [SerializeField] private UISpawner UISpawnerBottomStripe;


    private Sequence sequence;
    private Tween topStripeTween;
    private Tween bottomStripeTween;

    private Vector2 leftPosition;
    private Vector2 rightPosition;
    private Vector2 iconPosition;
    private Vector2 textPosition;
    private Vector2 topStripePosition;
    private Vector2 bottomStripePosition;

    private CanvasGroup popupGroup;
    private bool initialized;

    protected override void Awake()
    {
        base.Awake();
        popupGroup = GetComponent<CanvasGroup>();

        if (popupGroup == null)
            popupGroup = gameObject.AddComponent<CanvasGroup>();

        Initialize();
    }

    private void Initialize()
    {
        leftPosition = bannerLeft.anchoredPosition;
        rightPosition = bannerRight.anchoredPosition;
        iconPosition = bossIcon.anchoredPosition;
        textPosition = bossText.anchoredPosition;
        UISpawnerArrowRight.Spawn();
        UISpawnerArrowLeft.Spawn();
        UISpawnerBottomStripe.Spawn();
        UISpawnerTopStripe.Spawn();
        if (topStripe != null)
            topStripePosition = topStripe.anchoredPosition;

        if (bottomStripe != null)
            bottomStripePosition = bottomStripe.anchoredPosition;

        initialized = true;
        ResetAnimation();
    }

    private void ResetAnimation()
    {
        if (!initialized) return;

        popupGroup.alpha = 1f;
        popupGroup.blocksRaycasts = false;
        popupGroup.interactable = false;

        darkOverlay.alpha = 0f;

        bannerLeft.anchoredPosition =
            leftPosition + Vector2.left * bannerDistance;

        bannerRight.anchoredPosition =
            rightPosition + Vector2.right * bannerDistance;

        bossIcon.anchoredPosition = iconPosition;
        bossText.anchoredPosition = textPosition;

        bossIcon.localScale = Vector3.zero;
        bossText.localScale = Vector3.zero;

        bossIcon.localRotation = Quaternion.identity;
        bossText.localRotation = Quaternion.identity;

        if (topStripe != null)
            topStripe.anchoredPosition = topStripePosition;

        if (bottomStripe != null)
            bottomStripe.anchoredPosition = bottomStripePosition;
    }

    // ==========================================
    // WARNING STRIPES ANIMATION
    // ==========================================

    private void PlayStripes()
    {
        StopStripes();

        float moveTime = Mathf.Max(0.01f, stripeDuration);

        if (topStripe != null)
        {
            topStripe.anchoredPosition = topStripePosition;

            topStripeTween = topStripe
                .DOAnchorPosX(
                    topStripePosition.x + stripeMoveDistance,
                    moveTime
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }

        if (bottomStripe != null)
        {
            bottomStripe.anchoredPosition = bottomStripePosition;

            bottomStripeTween = bottomStripe
                .DOAnchorPosX(
                    bottomStripePosition.x - stripeMoveDistance,
                    moveTime
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }
    }

    private void StopStripes()
    {
        topStripeTween?.Kill();
        bottomStripeTween?.Kill();

        topStripeTween = null;
        bottomStripeTween = null;
    }

    // ==========================================
    // MAIN ANIMATION
    // ==========================================

    public void Play()
    {
        sequence?.Kill();
        StopStripes();

        transform.DOKill();
        bannerLeft.DOKill();
        bannerRight.DOKill();
        bossIcon.DOKill();
        bossText.DOKill();
        darkOverlay.DOKill();

        ResetAnimation();

        // Start moving warning stripes
        PlayStripes();

        sequence = DOTween.Sequence()
            .SetUpdate(true);

        // 1. Dark overlay
        sequence.Append(
            darkOverlay.DOFade(darkAlpha, 0.15f)
        );

        // 2. Banner slide in
        sequence.Append(
            bannerLeft.DOAnchorPos(
                leftPosition, duration
            ).SetEase(Ease.OutBack)
        );

        sequence.Join(
            bannerRight.DOAnchorPos(
                rightPosition, duration
            ).SetEase(Ease.OutBack)
        );

        // 3. Boss icon pop
        sequence.Append(
            bossIcon.DOScale(1.25f, 0.15f)
                .SetEase(Ease.OutQuad)
        );

        sequence.Append(
            bossIcon.DOScale(1f, 0.12f)
                .SetEase(Ease.OutBack)
        );

        // 4. Text impact
        sequence.Append(
            bossText.DOScale(1.3f, 0.12f)
                .SetEase(Ease.OutQuad)
        );

        sequence.Append(
            bossText.DOScale(1f, 0.15f)
                .SetEase(Ease.OutBack)
        );

        // 5. Shake icon and text
        sequence.Append(
            bossIcon.DOShakeAnchorPos(
                0.25f, 15f, 20, 90f
            )
        );

        sequence.Join(
            bossText.DOShakeAnchorPos(
                0.25f, 12f, 20, 90f
            )
        );

        // 6. Pulse
        sequence.Append(
            bossIcon.DOScale(1.1f, 0.12f)
        );

        sequence.Join(
            bossText.DOScale(1.05f, 0.12f)
        );

        sequence.Append(
            bossIcon.DOScale(1f, 0.12f)
        );

        sequence.Join(
            bossText.DOScale(1f, 0.12f)
        );

        // 7. Hold
        sequence.AppendInterval(holdTime);

        // 8. Exit
        sequence.Append(
            bannerLeft.DOAnchorPos(
                leftPosition + Vector2.left * bannerDistance,
                0.25f
            ).SetEase(Ease.InBack)
        );

        sequence.Join(
            bannerRight.DOAnchorPos(
                rightPosition + Vector2.right * bannerDistance,
                0.25f
            ).SetEase(Ease.InBack)
        );

        sequence.Join(
            bossIcon.DOScale(0f, 0.2f)
        );

        sequence.Join(
            bossText.DOScale(0f, 0.2f)
        );

        sequence.Join(
            darkOverlay.DOFade(0f, 0.25f)
        );

        sequence.OnComplete(() =>
        {
            StopStripes();
            popupGroup.alpha = 0f;
            sequence = null;
        });
    }

    private void OnDisable()
    {
        sequence?.Kill();
        sequence = null;
        StopStripes();
    }

    private void OnDestroy()
    {
        sequence?.Kill();
        StopStripes();
    }
}
