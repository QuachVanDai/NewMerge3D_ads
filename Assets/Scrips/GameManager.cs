using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SpawnInGrid gridPrefab;
    [SerializeField] private Transform environment;
    [SerializeField] private Transform friendlySpawnPoint;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform handTutorial;
    [SerializeField] private Vector2 FormationSize;
    private SpawnInGrid friendlySpawnInGrid;
    private SpawnInGrid enemySpawnInGrid;
    public Transform UnitPlacement;
    public AudioClip soundWin;
    public AudioClip soundLose;
    public AudioClip soundBg;
    public AudioSource audioSource;
    [SerializeField] private StateGame stateGame;
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
                HandMerge();
                break;
            case StateGame.PrepareFight:
                HandPrepareFight();
                break;
            case StateGame.Fight:
                HandFight();
                break;
            case StateGame.Lose:
                HandLose();
                break;
            case StateGame.Win:
                HandWin();
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
        PlayMusic(soundBg);
        //
        FriendlyGridManager.SetSpawnGrid(friendlySpawnInGrid);
        EnemyController.SetSpawnGrid(enemySpawnInGrid);
        yield return StartCoroutine(FriendlyGridManager.SpawnFriendlyTiles());
        MergeModeController.Init();
        EnemyController.Init();
        StartCoroutine(ZoomToBoss());
        yield return StartCoroutine(ZoomToBoss());
        yield return StartCoroutine(ZoomToBattle());
        yield return new WaitForSeconds(1f);
        StateGame = StateGame.Merge;
    }
    void HandMerge()
    {
        PlayMusic(soundBg);
        handTutorial.gameObject.SetActive(true);
        HomePopup.Instance.ShowPanelDragToMerge(true);
    }

    void HandPrepareFight()
    {
        handTutorial.gameObject.SetActive(false);
        HomePopup.Instance.ShowPanelDragToMerge(false);
        //
        MergeModeController.SetActiveCollider(true);
        HomePopup.Instance.ShowPanelFight(true);
    }
    void HandFight()
    {
        HomePopup.Instance.ShowPanelFight(false);
    }
    public void CheckWin()
    {
        Debug.Log("check win");
        if (MergeModeController.BaseUnits.Count == 0)
        {
            StateGame = StateGame.Lose;
        }
        else if (EnemyController.BaseUnits.Count == 0)
        {
            StateGame = StateGame.Win;
        }
    }
    void HandWin()
    {
        StartCoroutine(IE());
        IEnumerator IE()
        {
            MergeModeController.Dance();
            yield return new WaitForSeconds(1f);
            HomePopup.Instance.ShowPanelEndCard(EndCardType.Win);
            PlayMusic(soundWin);
        }
    }

    void HandLose()
    {
        StartCoroutine(IE());
        IEnumerator IE()
        {
            EnemyController.Dance();
            yield return new WaitForSeconds(1f);
            HomePopup.Instance.ShowPanelEndCard(EndCardType.Lose);
            PlayMusic(soundLose);
        }
    }
    void PlayMusic(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
        audioSource.loop = true;

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
        yield return new WaitForSeconds(1f);
        CameraController.Instance.SetTargetBoss();
        yield return new WaitForSeconds(1f);
    }
    IEnumerator ZoomToBattle()
    {
        yield return new WaitForSeconds(1.5f);
        CameraController.Instance.SetDefault();
        yield return new WaitForSeconds(1f);
    }
    public void Fight()
    {

    }
}
public enum StateGame
{
    Prepare,
    Merge,
    PrepareFight,
    Fight,
    Win,
    Lose
}
