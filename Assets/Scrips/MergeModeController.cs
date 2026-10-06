using System;
using System.Collections;
using System.Collections.Generic;
using ExampleProject.Gameplay.Faction;
using UnityEngine;

public class MergeModeController : Singleton<MergeModeController>
{
    [SerializeField] private List<UnitModeData> baseUnits;
    public List<UnitModeData> BaseUnits => baseUnits;


    FriendlyGridManager FriendlyGridManager => FriendlyGridManager.Instance;

    public void Init()
    {
        SortBaseUnitsBySpawnIndex();

        int j = 0;
        for (int i = 0; i < FriendlyGridManager.GetSpawnInGrid().TotalNumberOfPoints; i++)
        {
            if (j < baseUnits.Count && baseUnits[j].indexSpawn == i)
            {
                FriendlyGridManager.AssignUnitToSpawnGrid(baseUnits[j].unit, i);

                Vector3 _pos = FriendlyGridManager.GetTilePosition(baseUnits[j].indexSpawn);
                baseUnits[j].unit.SetLocalPosition(_pos);
                baseUnits[j].unit.Faction = FactionId.Friendly;
                baseUnits[j].unit.Init();
                j++;
            }

        }
    }

    public BaseUnit MergeUnit(int originIndex, int targetIndex)
    {
        RemoveUnit(originIndex);
        for (int i = 0; i < FriendlyGridManager.GetSpawnInGrid().TotalNumberOfPoints; i++)
        {
            if (baseUnits[i].indexSpawn == targetIndex)
            {
                FriendlyGridManager.AssignUnitToSpawnGrid(baseUnits[i].unit, targetIndex);
                FriendlyGridManager.PlaySpawnEffect( targetIndex);
                baseUnits[i].unit.Init();
                return baseUnits[i].unit;
            }
        }
        return null;
    }
    public void RemoveUnit(int _index)
    {
        for (int i = 0; i < FriendlyGridManager.GetSpawnInGrid().TotalNumberOfPoints; i++)
        {
            if (baseUnits[i].indexSpawn == _index)
            {
                baseUnits[i].unit.gameObject.SetActive(false);
                baseUnits.RemoveAt(i);
                break;
            }
        }

    }
    private void SortBaseUnitsBySpawnIndex()
    {
        baseUnits?.Sort((left, right) => left.indexSpawn.CompareTo(right.indexSpawn));
    }

    public void SetActiveCollider(bool isActive = true)
    {
        for (int i = 0; i < baseUnits.Count; i++)
        {
            baseUnits[i].unit.gameObject.GetComponent<Collider>().enabled = isActive;
        }
    }
}
[Serializable]
public class UnitModeData
{
    public BaseUnit unit;
    public int indexSpawn;
}
