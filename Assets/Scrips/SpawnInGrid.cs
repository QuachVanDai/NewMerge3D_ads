using UnityEngine;

public enum GridStartCorner
{
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight
}

public class SpawnInGrid : MonoBehaviour
{
    #region Fields

    [SerializeField] Vector3 center;
    [SerializeField] int rows = 5;
    [SerializeField] int columns = 5;
    [SerializeField] float spacingX = 1f;
    [SerializeField] float spacingY = 1f;
    [SerializeField] Vector3 offset;
    [SerializeField] GridStartCorner startCorner = GridStartCorner.TopLeft;

    [Header("Gizmos")]
    [SerializeField] bool drawGizmos = true;
    [SerializeField] Color gizmoGridColor = new(0f, 1f, 0f, 0.25f);
    [SerializeField] Color gizmoPointColor = new(1f, 1f, 1f, 0.75f);

    #endregion

    #region Properties

    public int TotalNumberOfPoints => rows * columns;
    public Quaternion GetQuaternion => transform.rotation;

    #endregion

    #region Public Methods

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    public void SetGridSize(Vector2 size)
    {
        rows = Mathf.Max(1, (int)size.y);
        columns = Mathf.Max(1, (int)size.x);
    }
    public void SetPosition(Vector3 vector3)
    {
        this.transform.position = vector3;
    }
    public void SetGridSpacing(float _spacingX, float _spacingY)
    {
        spacingX = Mathf.Max(0.01f, _spacingX);
        spacingY = Mathf.Max(0.01f, _spacingY);
    }
    public void SetStartCorner(GridStartCorner corner)
    {
        startCorner = corner;
    }
    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public Vector3 GetSpawnPosition(int _index)
    {
        int _maxIndex = rows * columns - 1;
        _index = Mathf.Clamp(_index, 0, _maxIndex);

        int _row = _index / columns;
        int _column = _index % columns;

        int _rowFromBottom = _row;
        int _columnFromLeft = _column;

        switch (startCorner)
        {
            case GridStartCorner.BottomRight:
                _columnFromLeft = columns - 1 - _column;
                break;
            case GridStartCorner.TopLeft:
                _rowFromBottom = rows - 1 - _row;
                break;
            case GridStartCorner.TopRight:
                _rowFromBottom = rows - 1 - _row;
                _columnFromLeft = columns - 1 - _column;
                break;
        }

        Vector3 _bottomLeft = GetBottomLeft();
        return _bottomLeft + transform.right * (_columnFromLeft * spacingX) + transform.forward * (_rowFromBottom * spacingY);
    }
    public Vector2Int GetPositionIndex(int _index)
    {
        int _maxIndex = rows * columns - 1;
        _index = Mathf.Clamp(_index, 0, _maxIndex);

        int _row = _index / columns;
        int _column = _index % columns;

        int _rowFromBottom = _row;
        int _columnFromLeft = _column;

        switch (startCorner)
        {
            case GridStartCorner.BottomRight:
                _columnFromLeft = columns - 1 - _column;
                break;
            case GridStartCorner.TopLeft:
                _rowFromBottom = rows - 1 - _row;
                break;
            case GridStartCorner.TopRight:
                _rowFromBottom = rows - 1 - _row;
                _columnFromLeft = columns - 1 - _column;
                break;
        }

        int _rowFromStart = _rowFromBottom;
        int _columnFromStart = _columnFromLeft;

        switch (startCorner)
        {
            case GridStartCorner.BottomRight:
                _columnFromStart = columns - 1 - _columnFromLeft;
                break;
            case GridStartCorner.TopLeft:
                _rowFromStart = rows - 1 - _rowFromBottom;
                break;
            case GridStartCorner.TopRight:
                _rowFromStart = rows - 1 - _rowFromBottom;
                _columnFromStart = columns - 1 - _columnFromLeft;
                break;
        }

        return new Vector2Int(_columnFromStart, _rowFromStart);
    }
    public Vector3 GetRandomSpawnPosition()
    {
        int _totalPoints = rows * columns;
        int _randomIndex = Random.Range(0, _totalPoints);
        return GetSpawnPosition(_randomIndex);
    }
    public Vector3 GetSpawnPosition(int _row, int _column)
    {
        var _index = _row * columns + _column;
        return GetSpawnPosition(_index);
    }


    #endregion

    #region Private Methods

    Vector3 GetWorldCenter()
    {
        return transform.TransformPoint(center + offset);
    }

    Vector3 GetBottomLeft()
    {
        float _width = (columns - 1) * spacingX;
        float _depth = (rows - 1) * spacingY;
        Vector3 _worldCenter = GetWorldCenter();

        return _worldCenter - transform.right * (_width * 0.5f) - transform.forward * (_depth * 0.5f);
    }

    void DrawGizmos()
    {
        float _width = (columns - 1) * spacingX;
        float _depth = (rows - 1) * spacingY;

        Matrix4x4 _previousMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = gizmoGridColor;
        Gizmos.DrawWireCube(center + offset, new Vector3(_width, 0f, _depth));

        Gizmos.color = gizmoPointColor;
        int _totalPoints = rows * columns;
        for (int _i = 0; _i < _totalPoints; _i++)
        {
            int _row = _i / columns;
            int column = _i % columns;

            Vector3 _localBottomLeft = (center + offset) - Vector3.right * (_width * 0.5f) - Vector3.forward * (_depth * 0.5f);
            Vector3 _localPos = _localBottomLeft + Vector3.right * (column * spacingX) + Vector3.forward * (_row * spacingY);

            float _gizmoSize = (spacingX + spacingY) * 0.05f;
            Gizmos.DrawSphere(_localPos, _gizmoSize);
        }

        Gizmos.matrix = _previousMatrix;
    }

    #endregion

    #region LifeCycle

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
        {
            return;
        }

        DrawGizmos();
    }

    #endregion
}