using System;
using UnityEditor;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    
    private SessionState _sessionState;
    public SessionState SessionState => _sessionState;

    private Spawner _spawner;

    private int _countBall = 3;
    private int _score = 0;
    public int Score => _score;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    
        DontDestroyOnLoad(gameObject);

        _spawner = FindObjectOfType<Spawner>();
        
        Dispatcher.OnStart += OnStart;
        Dispatcher.OnRestart += OnRestart;
        Dispatcher.OnNextLevel += OnNextLevel;
        Dispatcher.OnLose += OnLose;
        Dispatcher.OnBrickDestroy += OnChangeBricks;
        Dispatcher.OnScoreAdd += OnScoreAdd;
        _sessionState = SessionState.MENU;
    }

    private void OnStart()
    {
        _spawner.Generation();
        _sessionState = SessionState.GAME;
        Time.timeScale = 1;
    }

    private void OnRestart()
    {
        _countBall = 3;
        _score = 0;
        OnStart();
    }
    
    private void OnNextLevel()
    {
        OnStart();
    }

    private void OnLose()
    {
        _sessionState = SessionState.LOSE;
    }

    private void OnChangeBricks()
    {
        if (_spawner.Bricks.Count > 0) return;
        
        Time.timeScale = 0;
        _sessionState = SessionState.WIN;
        Dispatcher.Send(Event.ON_WIN);
    }

    private void OnScoreAdd()
    {
        _score++;
    }

        private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Escape)) return;
        
        switch (_sessionState)
        {
            case SessionState.GAME:
                Time.timeScale = 0;
                _sessionState = SessionState.PAUSE;
                break;
            case SessionState.PAUSE:
                Time.timeScale = 1;
                _sessionState = SessionState.GAME;
                break;
        }
    }

    private void OnDestroy()
    {
        Dispatcher.OnStart -= OnStart;
        Dispatcher.OnRestart -= OnRestart;
        Dispatcher.OnNextLevel -= OnNextLevel;
        Dispatcher.OnLose -= OnLose;
        Dispatcher.OnBrickDestroy -= OnChangeBricks;
        Dispatcher.OnScoreAdd -= OnScoreAdd;
    }
}
