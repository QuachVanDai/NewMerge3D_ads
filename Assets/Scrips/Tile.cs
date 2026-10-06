using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using VTLTools.Effect;

public class Tile : MonoBehaviour
{
    #region Fields

    [SerializeField] Effect spawnEffect;
    [SerializeField] GameObject meleeMergeableEffect;
    [SerializeField] GameObject rangedMergeableEffect;
    [SerializeField] Renderer tileRenderer;
    [SerializeField, ReadOnly] BaseUnit unitOnTile;
    [ReadOnly] public int col;
    [ReadOnly] public int row;

    #endregion

    #region Properties

    public bool IsHaveCharacter => unitOnTile != null;
    public BaseUnit CurrentCharacter => unitOnTile;
    public Vector2Int IndexPosition => new(col, row);
    public Vector3 Position => this.transform.position;
    public Renderer TileRenderer => tileRenderer;
    public int Index { get; private set; }


    #endregion

    #region LifeCycle

    void OnEnable()
    {
        // EventDispatcher.Instance.AddListener(EventName.OnBattlePrepare, OnBattlePrepareListener);
        EventDispatcher.Instance.AddListener(EventName.OnHoldCharacter, OnHoldCharacterListener);
        // EventDispatcher.Instance.AddListener(EventName.OnReleaseCharacter, OnReleaseCharacterListener);
        // EventDispatcher.Instance.AddListener(EventName.OnAddUnit, OnAddUnitListener);
        // EventDispatcher.Instance.AddListener(EventName.OnBattleStart, OnBattleStartListener);
    }
    void OnDisable()
    {
        // EventDispatcher.Instance.RemoveListener(EventName.OnBattlePrepare, OnBattlePrepareListener);
        EventDispatcher.Instance.RemoveListener(EventName.OnHoldCharacter, OnHoldCharacterListener);
        // EventDispatcher.Instance.RemoveListener(EventName.OnReleaseCharacter, OnReleaseCharacterListener);
        // EventDispatcher.Instance.RemoveListener(EventName.OnAddUnit, OnAddUnitListener);
        // EventDispatcher.Instance.RemoveListener(EventName.OnBattleStart, OnBattleStartListener);
    }

    #endregion

    #region Private Methods

    // void OnBattlePrepareListener(EventName key, object data)
    // {
    //     CheckMergeableEffect();
    // }
    void OnHoldCharacterListener(EventName key, object data)
    {
        CheckMergeableWithHoldingCharacter();
    }
    // void OnReleaseCharacterListener(EventName key, object data)
    // {
    //     CheckMergeableEffect();
    // }
    // void OnAddUnitListener(EventName key, object data)
    // {
    //     CheckMergeableEffect();
    // }
    // void OnBattleStartListener(EventName key, object data)
    // {
    //     SetEffect(false);
    // }
    void CheckMergeableEffect()
    {
        // bool _isMergeable = FormationProgress.IsCanMergeWithAnyOtherGrid(Index);
        // SetEffect(_isMergeable);
    }
    void CheckMergeableWithHoldingCharacter()
    {
        // var _holdingIndex = TouchInputController.Instance.originFriendlyTileIndex;
        // if (_holdingIndex == Index)
        // {
        //     SetEffect(false);
        //     return;
        // }
        // bool _isMergeable = FormationProgress.IsCanMerge(Index, _holdingIndex);
        // SetEffect(_isMergeable);
    }
    void SetEffect(bool _isShow)
    {
        // if (!_isShow)
        // {
        //     meleeMergeableEffect.SetActive(false);
        //     rangedMergeableEffect.SetActive(false);
        //     return;
        // }

        // var _unitType = Units.GetUnitData(unitOnTile.UnitId).unitType;
        // meleeMergeableEffect.SetActive(_isShow && _unitType == UnitType.Melee);
        // rangedMergeableEffect.SetActive(_isShow && _unitType == UnitType.Ranged);
    }

    #endregion

    #region Public Methods

    public void SetCurrentCharacter(BaseUnit _unit)
    {
        unitOnTile = _unit;
    }

    public BaseUnit GetCurrentCharacter()
    {
        return unitOnTile;
    }
    public void Init(Transform _parent, Vector3 _localPos, Quaternion _quaternion, int _col, int _row, int _index)
    {
        this.transform.parent = _parent;
        this.transform.localPosition = _localPos;
        this.transform.rotation = _quaternion;
        this.name = this.name + _col + _row;
        col = _col;
        row = _row;
        Index = _index;
    }

    // public void ClearGrid(EventName _key = EventName.NONE, object _data = null)
    // {
    //     if (unitOnTile == null)
    //         return;

    //     unitOnTile.SelfDestroy();
    //     unitOnTile = null;
    // }
    public void PlaySpawnEffect()
    {
        //spawnEffect.Play();
    }

    #endregion
}
