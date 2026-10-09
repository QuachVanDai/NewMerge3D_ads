using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SpawnInGrid gridPrefab;
    [SerializeField] private Transform environment;
    [SerializeField] private Transform friendlySpawnPoint;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform handTutorial;
    [SerializeField] private GameObject gate;
    [SerializeField] private Vector2 FormationSize;
    private SpawnInGrid friendlySpawnInGrid;
    private SpawnInGrid enemySpawnInGrid;
    public Transform UnitPlacement;
    public AudioClip soundWin;
    public AudioClip soundLose;
    public AudioClip soundBg;
    public AudioSource audioSource;
    [LunaPlaygroundField("hpMeeNormal", 1, "Game Settings")] public float hpMeeNormal = 1000;
    [LunaPlaygroundField("hpSpiderMan", 2, "Game Settings")] public float hpSpiderMan = 3000;
    [LunaPlaygroundField("hpBoss", 3, "Game Settings")] public float hpBoss = 1500;
    [LunaPlaygroundField("hpEnemy", 4, "Game Settings")] public float hpEnemy = 1000;
    [LunaPlaygroundField("damageMeeNormal", 5, "Game Settings")] public float damageMeeNormal = 150;
    [LunaPlaygroundField("damageSpiderMan", 6, "Game Settings")] public float damageSpiderMan = 300;
    [LunaPlaygroundField("damageBoss", 7, "Game Settings")] public float damageBoss = 275;
    [LunaPlaygroundField("damageEnemy", 8, "Game Settings")] public float damageEnemy = 125;
    [LunaPlaygroundField("damageBuf", 9, "Game Settings")] public int damageBuf = 10;
    [LunaPlaygroundField("timeScale", 10, "Game Settings")] public float timeScale = 1.5f;

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
        FriendlyGridManager.SetAllColliderTiles();
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
        Time.timeScale = timeScale;
    }
    public void CheckWin()
    {
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
            gate.SetActive(false);

            Time.timeScale = 1;
            MergeModeController.Dance();
            yield return new WaitForSeconds(2f);
            HomePopup.Instance.ShowPanelEndCard(EndCardType.Win);
            PlayMusic(soundWin);
        }
    }

    void HandLose()
    {
        StartCoroutine(IE());
        IEnumerator IE()
        {
            gate.SetActive(false);

            Time.timeScale = 1;
            EnemyController.Dance();
            yield return new WaitForSeconds(2f);
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
        BossIncomingAnimation.Instance.Play();
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
