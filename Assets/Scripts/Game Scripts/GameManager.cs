using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using SkyReach.Player;

/// <summary>
/// The game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private FadeToBlack levelChanger;

    [Header("Scene References")]
    [SerializeField] private int winSceneIndex;

    // internal variables
    private GameState _state;
    private Scene _currentScene;
    private PlayerController _playerController;
    private GrapplingHook _grapplingHook;

    // exposed properties
    public GameState State
    {
        get => _state;
        private set
        {
            _state = value;

            // call specific events
            switch (_state)
            {
                case GameState.Playing:
                    PlayerSpawned?.Invoke(); // only fire start event when done starting
                    break;
                case GameState.Paused:
                    GamePaused?.Invoke();
                    break;
                case GameState.Won:
                    GameWon?.Invoke();
                    break;
                case GameState.Death:
                    PlayerDied?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            StateChanged?.Invoke(_state);

        }
    }

    // events
    public static event Action<GameState> StateChanged;
    public static event Action PlayerSpawned;
    public static event Action GameWon;
    public static event Action GamePaused;
    public static event Action PlayerDied;

    
    private void Awake()
    {
        // Implementing Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // set player controller and grappling hook
        _playerController = player.GetComponent<PlayerController>();
        _grapplingHook = player.GetComponent<GrapplingHook>();
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.Starting:
                EnablePlayer();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Death:
                break;
            case GameState.Won:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void DisablePlayer()
    {
        // get playercontroller, grapplinghook, and disable them
    }

    public static void EnablePlayer()
    {
        // get playercontroller, grapplinghook, and enable them

    }

    public static void WinGame()
    {
        Instance.State = GameState.Won;
        Instance.levelChanger.FadeToLevel(Instance.winSceneIndex);
    }

    public static void KillPlayer()
    {
        Instance.State = GameState.Death;
        Instance.levelChanger.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum GameState
{
    Starting,
    Playing,
    Paused,
    Death,
    Won
}