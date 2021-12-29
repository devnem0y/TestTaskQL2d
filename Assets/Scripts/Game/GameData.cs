using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private int _complexitySelectId = 1;
    [SerializeField] private List<СomplexityData> _сomplexityDatas;
    public СomplexityData СomplexityData => _сomplexityDatas[_complexitySelectId];

    private SessionState _sessionState;
    public SessionState SessionState => _sessionState;

    private Spawner _spawner;

    private int _countBall;
    public int CountBall => _countBall;

    private int _score;
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
        Dispatcher.OnNextLevel += OnStart;
        Dispatcher.OnBrickDestroy += OnChangeBricks;
        Dispatcher.OnFailing += OnFailing;
        Dispatcher.OnComplexityChange += OnComplexityChange;
        _sessionState = SessionState.MENU;
    }

    private void OnStart()
    {
        _spawner.Generation();
        _countBall = 3;
        Dispatcher.Send(Event.ON_SET_COUNT_BALL);
        Time.timeScale = 1;
        _sessionState = SessionState.GAME;
    }

    private void OnRestart()
    {
        _score = 0;
        OnStart();
    }

    private void OnChangeBricks()
    {
        _score++;
        Dispatcher.Send(Event.ON_SCORE_ADD);
        
        if (_spawner.Bricks.Count > 0) return;
        
        Time.timeScale = 0;
        _sessionState = SessionState.WIN;
        Dispatcher.Send(Event.ON_WIN);
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
    
    private void OnComplexityChange(object arg)
    {
        _complexitySelectId = (int) arg;
        Dispatcher.Send(Event.ON_SETUP_PARAM);
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
        Dispatcher.OnNextLevel -= OnRestart;
        Dispatcher.OnBrickDestroy -= OnChangeBricks;
        Dispatcher.OnFailing -= OnFailing;
        Dispatcher.OnComplexityChange -= OnComplexityChange;
    }
}
