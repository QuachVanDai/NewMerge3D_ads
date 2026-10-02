using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : Singleton<CursorControl>
{
    #region Fields

    [SerializeField] Cursor cursor;

    [SerializeField] Camera cam;
    [SerializeField] bool isCursorPlacementValid;

    [SerializeField] Vector3 placementPosition;
    [SerializeField] Quaternion placementRotation;

    [SerializeField] Plane placementPlane = new(Vector3.up, Vector3.zero);

    #endregion

    #region Properties

    public Vector3 PlacementPosition
    {
        get => placementPosition;
        private set => placementPosition = value;
    }

    public Quaternion PlacementRotation
    {
        get => placementRotation;
        private set => placementRotation = value;
    }

    public bool IsCursorPlacementValid
    {
        get => isCursorPlacementValid;
        private set => isCursorPlacementValid = value;
    }

    public Transform CursorTransform => cursor.transform;

    #endregion

    #region LifeCycle

    private void Update()
    {
        UpdatePlacementPoseWithPlaneRaycast();
        UpdatePlacementCursor();
    }

    #endregion

    #region Private Methods

    private void UpdatePlacementCursor()
    {
        cursor.transform.SetPositionAndRotation(
            PlacementPosition,
            PlacementRotation
        );
    }

    Ray _ray;
    Vector2 _pointerPosition;
    Vector3 _hitPosition;
    Quaternion _hitRotation;

    void UpdatePlacementPoseWithPlaneRaycast()
    {
        _pointerPosition = Input.mousePosition;
        _ray = cam.ScreenPointToRay(_pointerPosition);

        if (placementPlane.Raycast(_ray, out float enter))
        {
            IsCursorPlacementValid = true;

            _hitPosition = _ray.GetPoint(enter);
            _hitRotation = Quaternion.identity;

            PlacementPosition = _hitPosition;
            PlacementRotation = _hitRotation;
        }
        else
        {
            IsCursorPlacementValid = false;
        }
    }

    #endregion
}