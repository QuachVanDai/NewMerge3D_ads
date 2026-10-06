using System;
using System.Collections;
using System.Collections.Generic;
using ExampleProject.Gameplay.Faction;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>
{
    [SerializeField] private List<UnitModeData> baseUnits;

    [SerializeField] SpawnInGrid enemySpawnInGrid;
    public List<UnitModeData> BaseUnits => baseUnits;
    public void SetSpawnGrid(SpawnInGrid _enemySpawnInGrid)
    {
        enemySpawnInGrid = _enemySpawnInGrid;
    }
    public void Init()
    {
        SortBaseUnitsBySpawnIndex();

        for (int i = 0; i < baseUnits.Count; i++)
        {
            Vector3 _pos = enemySpawnInGrid.GetSpawnPosition(baseUnits[i].indexSpawn);
            baseUnits[i].unit.SetLocalPosition(_pos);
            baseUnits[i].unit.SetLocalRotation(Vector3.up * 180);
            baseUnits[i].unit.Faction = FactionId.Enemy;
            baseUnits[i].unit.Init();
        }
    }

    private void OnValidate()
    {
        // SortBaseUnitsBySpawnIndex();
    }
public void RemoveUnit(BaseUnit _unit)
    {
        for (int i = 0; i < baseUnits.Count; i++)
        {
            if (baseUnits[i].unit == _unit)
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
}
