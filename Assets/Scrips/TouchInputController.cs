using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ExampleProject.Gameplay.Faction;
using UnityEngine;

public class TouchInputController : Singleton<TouchInputController>
{
    #region Fields

    [SerializeField] LayerMask gridTileLayerMask;
    [SerializeField] Camera cam;
    [SerializeField] BaseUnit chosenUnit;
    [SerializeField] public int originFriendlyTileIndex;
    [SerializeField] public bool isMergeFirstFinished;

    #endregion

    #region Properties

    FriendlyGridManager FriendlyGridManager => FriendlyGridManager.Instance;
    EventDispatcher EventDispatcher => EventDispatcher.Instance;
    CursorControl CursorControl => CursorControl.Instance;
    MergeModeController MergeModeController => MergeModeController.Instance;
    GameManager GameManager => GameManager.Instance;
    // MergeTutorialManager MergeTutorialManager => MergeTutorialManager.Instance;

    #endregion

    #region LifeCycle


    void Update()
    {
        if (GameManager.StateGame != StateGame.Merge)
            return;
        UpdateCenterGrid();
    }

    #endregion

    #region Private Methods

    Ray _ray;
    RaycastHit _hit;
    Vector2 _pointerPosition;
    [SerializeField] int _currentFriendlyTileIndex = -1;
    void UpdateCenterGrid()
    {
        bool _isPressedThisFrame;
        bool _isPressed;
        bool _isReleasedThisFrame;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _pointerPosition = touch.position;
            _isPressedThisFrame = touch.phase == TouchPhase.Began;
            _isPressed = touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
            _isReleasedThisFrame = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
        }
        else
        {
            _pointerPosition = Input.mousePosition;
            _isPressedThisFrame = Input.GetMouseButtonDown(0);
            _isPressed = Input.GetMouseButton(0);
            _isReleasedThisFrame = Input.GetMouseButtonUp(0);
        }

        if (_isPressedThisFrame)
        {
            if (MouseHoverUI.IsPointerOverUIElement())
                return;

            _ray = cam.ScreenPointToRay(_pointerPosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.yellow);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity)
                    && _hit.transform.TryGetComponent(out BaseUnit chosenCharacter)
                    && chosenCharacter.Faction == FactionId.Friendly)
                PickUpCharacter(chosenCharacter);
        }
        if (_isPressed)
        {
            if (MouseHoverUI.IsPointerOverUIElement())
                return;

            _ray = cam.ScreenPointToRay(_pointerPosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.blue);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, gridTileLayerMask)
                    && _hit.transform.TryGetComponent(out Tile tile))
            {
                _currentFriendlyTileIndex = tile.Index;
            }
            else
                _currentFriendlyTileIndex = -1;
        }
        if (_isReleasedThisFrame)
        {
            ReleaseCharacter();
            _currentFriendlyTileIndex = -1;
        }
    }
    void PickUpCharacter(BaseUnit _unit)
    {
        chosenUnit = _unit;
        chosenUnit.PlayPickUpAnim();
        originFriendlyTileIndex = FriendlyGridManager.GetUnitTile(chosenUnit).Index;
        SetCursorParentCharacter(chosenUnit.transform);
        EventDispatcher.Dispatch(EventName.OnHoldCharacter, chosenUnit);
    }
    void ReleaseCharacter()
    {
        if (chosenUnit == null)
            return;

        if (_currentFriendlyTileIndex == -1)
        {
            _BackToOriginGrid();
            return;
        }

        if (_currentFriendlyTileIndex == originFriendlyTileIndex)
        {
            _BackToOriginGrid();
            return;
        }
        if (!FriendlyGridManager.IsEmpty(_currentFriendlyTileIndex))
        {
            _MoveUnitToEmptyGrid(_currentFriendlyTileIndex);
            return;
        }

        if (!FriendlyGridManager.IsCanMerge(originFriendlyTileIndex, _currentFriendlyTileIndex))
        {
            _BackToOriginGrid();
            return;
        }

        _MergeTwoUnit();

        void _BackToOriginGrid()
        {
            Vector3 _originPos = FriendlyGridManager.GetTilePosition(originFriendlyTileIndex);
            SetNewParentCharacter(chosenUnit.transform, _originPos);
            FriendlyGridManager.SetUnitToTile(chosenUnit, originFriendlyTileIndex);
            chosenUnit.PlayIdleAnim();
            chosenUnit = null;
            EventDispatcher.Dispatch(EventName.OnReleaseCharacter, chosenUnit);
        }
        void _MoveUnitToEmptyGrid(int _emptyTileIndex)
        {
            var _emptyTilePos = FriendlyGridManager.GetTilePosition(_emptyTileIndex);
            SetNewParentCharacter(chosenUnit.transform, _emptyTilePos);
            FriendlyGridManager.SetUnitToTile(chosenUnit, _emptyTileIndex);
            FriendlyGridManager.SetUnitToTile(null, originFriendlyTileIndex);

            //  MergeModeController.SwapUnit(_emptyTileIndex, originFriendlyTileIndex);
            chosenUnit.PlayIdleAnim();
            chosenUnit = null;
            EventDispatcher.Dispatch(EventName.OnReleaseCharacter, chosenUnit);


        }
        void _MergeTwoUnit()
        {
            var _newUnit = MergeModeController.MergeUnit(originFriendlyTileIndex, _currentFriendlyTileIndex);
            FriendlyGridManager.SetUnitToTile(_newUnit, _currentFriendlyTileIndex);
            FriendlyGridManager.SetUnitToTile(null, originFriendlyTileIndex);
            chosenUnit = null;
            EventDispatcher.Dispatch(EventName.OnReleaseCharacter, chosenUnit);
            isMergeFirstFinished = true;
            GameManager.StateGame = StateGame.PrepareFight;
        }
    }

    #endregion

    #region Public Methods

    public void SetCursorParentCharacter(Transform _obj, Action _action = null)
    {
        _obj.parent = CursorControl.CursorTransform;
        _obj.DOKill();
        _obj.DOLocalMove(new Vector3(0, 0.2f, 0), 0.3f).OnComplete(() => _action?.Invoke());
        _obj.DOLocalRotate(Vector3.zero, 0.3f);
    }
    public void SetNewParentCharacter(Transform _obj, Vector3 _newPos, Action _action = null)
    {
        _obj.parent = GameManager.Instance.UnitPlacement;
        _obj.DOKill();
        _obj.DOMove(_newPos, 0.3f).OnComplete(() => _action?.Invoke());
        _obj.DOLocalRotate(Vector3.zero, 0.3f);
    }

    #endregion
}
