using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FriendlyGridManager : Singleton<FriendlyGridManager>
{
    #region Fields

    [SerializeField] Tile friendlyTilePrefab;
    [SerializeField] List<Tile> friendlyTiles = new();
    SpawnInGrid friendlySpawnInGrid;
    [SerializeField] Transform otherPlacement;

    #endregion

    #region Properties



    #endregion

    #region LifeCycle



    #endregion

    #region Private Methods



    #endregion

    #region Public Methods

    public void SetSpawnGrid(SpawnInGrid _gruntSpawnInGrid)
    {
        friendlySpawnInGrid = _gruntSpawnInGrid;
    }
    public IEnumerator SpawnFriendlyTiles()
    {
        var _parent = otherPlacement;
        for (int i = 0; i < friendlySpawnInGrid.TotalNumberOfPoints; i++)
        {
            var _spawnPos = friendlySpawnInGrid.GetSpawnPosition(i);
            var _spawnRot = friendlySpawnInGrid.GetQuaternion;
            var _index = friendlySpawnInGrid.GetPositionIndex(i);
            var _tileInstance = Instantiate(friendlyTilePrefab);
            friendlyTiles.Add(_tileInstance);
            _tileInstance.Init(_parent, _spawnPos, _spawnRot, _index.x, _index.y, i);
            yield return null;
        }
    }
    public SpawnInGrid GetSpawnInGrid()
    {
        return friendlySpawnInGrid;
    }
    public void AssignUnitToSpawnGrid(BaseUnit _unit, int _index)
    {
        friendlyTiles[_index].SetCurrentCharacter(_unit);
    }
    public Tile GetUnitTile(BaseUnit _unit)
    {
        foreach (var _tile in friendlyTiles)
        {
            if (_tile.CurrentCharacter == _unit)
                return _tile;
        }
        return null;
    }
    public Vector3 GetTilePosition(int _index)
    {
        return friendlyTiles[_index].Position;
    }
    public void SetUnitToTile(BaseUnit _unit, int _index)
    {
        var _tile = friendlyTiles[_index];
        _tile.SetCurrentCharacter(_unit);
    }
    public void PlaySpawnEffect(int _index)
    {
        var _tile = friendlyTiles[_index];
        _tile.PlaySpawnEffect();
    }
    public void ClearAllTiles()
    {
        foreach (var _tile in friendlyTiles)
        {
            //  _tile.ClearGrid();
        }
    }
    public void SetAllColliderTiles()
    {
        foreach (var _tile in friendlyTiles)
        {
            _tile.SetCollider();
        }
    }
    public Renderer GetTileRenderer(int _index)
    {
        return friendlyTiles[_index].TileRenderer;
    }

    public bool IsEmpty(int _index)
    {
        return friendlyTiles[_index].IsHaveCharacter;
    }
    public bool IsCanMerge(int _index1, int _index2)
    {
        if (friendlyTiles[_index1].CurrentCharacter != null && friendlyTiles[_index2].CurrentCharacter != null)
            return friendlyTiles[_index1].CurrentCharacter.unitID == friendlyTiles[_index2].CurrentCharacter.unitID;
        else
            return false;
    }
    #endregion
}
