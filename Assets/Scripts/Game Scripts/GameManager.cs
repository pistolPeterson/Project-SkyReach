using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("Scene References")]
    [SerializeField] private Scene winScene;

    // internal variables
    private GameState _state;
    private GameObject _player;
    private Scene _currentScene;

    // exposed properties
    public GameState State
    {
        get => _state;
        private set
        {
            _state = value;
            onStateChange.Invoke(_state);
        }
    }

    // events
    public static event Action<GameState> onStateChange;

    // Implementing Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.Starting:
                SpawnPlayer();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Death:
                break;
            case GameState.Completed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SpawnPlayer()
    {
        _player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        State = GameState.Playing;
    }

    private void WinGame()
    {
        State = GameState.Completed;
        SceneManager.LoadScene(winScene.name);
    }

    private void LoseGame()
    {
        State = GameState.Death;
        SceneManager.LoadScene(_currentScene.name);
    }
}

public enum GameState
{
    Starting,
    Playing,
    Paused,
    Death,
    Completed
}