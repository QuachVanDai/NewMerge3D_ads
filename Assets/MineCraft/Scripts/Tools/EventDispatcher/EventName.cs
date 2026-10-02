using System.Collections.Generic;

public enum EventName
{
    NONE,
    OnBeforeGameStateChange,
    OnAfterGameStateChange,

    OnUpdateCurrency,

    OnUnFocused,
    OnPauseGame,

    OnRemovedAds,

    InternetConnectivityChanged,
    OnInteractableDetected,
    OnInteractableLost,

    OnUnlockSkin,
    OnProcessSkinProgress,

    OnUnlockWeapon,
    OnProcessWeaponProgress,

    OnUnlockWing,
    OnProcessWingProgress,

    OnUnlockAvatar,
    OnProcessAvatarProgress,

    OnUnlockFrame,
    OnProcessFrameProgress,

    OnEquipSkin,
    OnEquipWing,
    OnEquipWeapon,
    OnEquipAvatar,
    OnEquipFrame,
    OnEquipTitle,

    OnClaimDailyReward,
    OnChangeShowingBanner,
    OnChangeShowingMrec,

    OnBackpackChangeShowingItem,
    OnEditProfileChangeShowingItem,
    OnPlayerFinishCalculateStats,

    OnCameraSensitivityChanged,
    OnMidnight,
    OnAdGiftWatched,

    OnProcessAchievementProgress,
    OnProcessDailyTaskProgress,
    LuckyWheelOutOfFree,

    OnItemAdded,
    OnStartCreativeServe,
    OnLikeOtherPlayer,
    OnChangeName,

    OnVictory,
    OnProcessTalentCardProgress,
    OnOfferCrystalAdActive,
    OnOfferCrystalIAPActive,

    OnBattlePrepare,
    OnBattleStart,
    OnHoldCharacter,
    OnReleaseCharacter,
    OnAddUnit,
    OnUnitDied,
    OnWaveChanged,
    OnUnlockUnit,
    OnProcessUnitProgress,
    OnUpgradeSlot,
    OnChosenCard,
    OnCancelChosenCard,
    OnFormationSaved,
    OnUpgradeUnitStar,
    OnGachaBoxOpened,
    OnClaimAdGift,
    OnDailyBuyableCurrenciesChanged,
    OnWatchAdChest,
    OnWatchDailyAdsShop,
    OnChapterRewardClaimed,
    TutorialStepCompleted,
}

public class EventTypeComparer : IEqualityComparer<EventName>
{
    public bool Equals(EventName x, EventName y)
    {
        return x == y;
    }

    public int GetHashCode(EventName t)
    {
        return (int)t;
    }
}
