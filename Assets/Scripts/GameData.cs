using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private int _complexitySelectId;
    private List<СomplexityData> _сomplexityDatas;
    public СomplexityData СomplexityData => _сomplexityDatas[_complexitySelectId];

    private SessionState _sessionState;
    public SessionState SessionState => _sessionState;

    private Spawner _spawner;

    private int _countBall = 3;
    public int CountBall => _countBall;

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
        Dispatcher.OnBrickDestroy += OnChangeBricks;
        Dispatcher.OnScoreAdd += OnScoreAdd;
        Dispatcher.OnFailing += OnFailing;
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
        _countBall = 3;
        OnStart();
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

    private void OnFailing()
    {
        _countBall--;
        Dispatcher.Send(Event.ON_SET_COUNT_BALL);

        if (_countBall == 0)
        {
            Time.timeScale = 0;
            _sessionState = SessionState.LOSE;
            Dispatcher.Send(Event.ON_LOSE);
        }
    }

    private void OnDestroy()
    {
        Dispatcher.OnStart -= OnStart;
        Dispatcher.OnRestart -= OnRestart;
        Dispatcher.OnNextLevel -= OnNextLevel;
        Dispatcher.OnBrickDestroy -= OnChangeBricks;
        Dispatcher.OnScoreAdd -= OnScoreAdd;
        Dispatcher.OnFailing -= OnFailing;
    }
}
