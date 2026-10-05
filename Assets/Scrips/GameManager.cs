using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SpawnInGrid gridPrefab;
    [SerializeField] private Transform environment;
    [SerializeField] private Transform friendlySpawnPoint;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Vector2 FormationSize;
    private SpawnInGrid friendlySpawnInGrid;
    private SpawnInGrid enemySpawnInGrid;
    public Transform UnitPlacement;
    private StateGame stateGame;
    public StateGame StateGame
    {
        get => stateGame;
        set
        {
            stateGame = value;
            ChangeState();
        }
    }

    FriendlyGridManager FriendlyGridManager => FriendlyGridManager.instance;
    MergeModeController MergeModeController => MergeModeController.instance;
    EnemyController EnemyController => EnemyController.instance;
    private void ChangeState()
    {
        switch (stateGame)
        {
            case StateGame.Prepare:
                StartCoroutine(Init());
                break;
            case StateGame.Merge:
                break;
            case StateGame.Fight:
                break;
            default:
                Debug.LogWarning($"State chưa xử lý: {stateGame}");
                break;
        }

    }
    private void Start()
    {
        StateGame = StateGame.Prepare;
    }

    public IEnumerator Init()
    {
        SpawnSpawnGrid();
        //
        FriendlyGridManager.SetSpawnGrid(friendlySpawnInGrid);
        EnemyController.SetSpawnGrid(enemySpawnInGrid);
        yield return StartCoroutine(FriendlyGridManager.SpawnFriendlyTiles());
        MergeModeController.Init();
        EnemyController.Init();
        StartCoroutine(ZoomToBoss());
    }
    void SpawnSpawnGrid()
    {
        // Instantiate the spawn grids for friendly and enemy units
        friendlySpawnInGrid = _SpawnSpawnGrid(GridStartCorner.TopLeft, friendlySpawnPoint.position, Quaternion.identity);

        // Instantiate the spawn grids for friendly and enemy units
        enemySpawnInGrid = _SpawnSpawnGrid(GridStartCorner.BottomRight, enemySpawnPoint.position, Quaternion.Euler(0, 180, 0));

        SpawnInGrid _SpawnSpawnGrid(GridStartCorner _startCorner, Vector3 _position, Quaternion _rotation)
        {
            var _spawnInGrid = Instantiate(gridPrefab);
            _spawnInGrid.SetParent(environment);
            _spawnInGrid.SetGridSize(FormationSize);
            _spawnInGrid.SetStartCorner(_startCorner);
            _spawnInGrid.SetPosition(_position);
            _spawnInGrid.SetRotation(_rotation);
            return _spawnInGrid;

        }
    }
    IEnumerator ZoomToBoss()
    {
        yield return null;

    }
    public void Fight()
    {

    }
}
public enum StateGame
{
    Prepare,
    Merge,
    Fight,
}
