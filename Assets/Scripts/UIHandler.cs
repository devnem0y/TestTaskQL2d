using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelLose;
    [SerializeField] private GameObject _panelHUD;
    
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnOptions;
    [SerializeField] private Button _btnExit;
    
    [SerializeField] private Button _btnBackToMenuO;
    [SerializeField] private Button _btnBackToMenuW;
    [SerializeField] private Button _btnBackToMenuL;
    
    [SerializeField] private Button _btnRestartW;
    [SerializeField] private Button _btnRestartL;
    [SerializeField] private Button _btnNextLevel;
    [SerializeField] private Button _btnAdvertisement;
    
    [SerializeField] private Text _scoreHUD;
    [SerializeField] private Text _scoreWin;
    [SerializeField] private Text _scoreLose;
    [SerializeField] private Text _countBall;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnPlay);
        _btnOptions.onClick.AddListener(OnOptions);
        _btnExit.onClick.AddListener(OnExit);
        _btnRestartW.onClick.AddListener(OnRestart);
        _btnRestartL.onClick.AddListener(OnRestart);
        _btnNextLevel.onClick.AddListener(OnNextLevel);
        _btnAdvertisement.onClick.AddListener(OnAdvertisement);
        _btnBackToMenuO.onClick.AddListener(OnBackToMenu);
        _btnBackToMenuW.onClick.AddListener(OnBackToMenu);
        _btnBackToMenuL.onClick.AddListener(OnBackToMenu);
        
        Dispatcher.OnBrickDestroy += OnScoreAdd;
        Dispatcher.OnSetCountBall += OnSetCountBall;
        Dispatcher.OnWin += OnWin;
        Dispatcher.OnLose += OnLose;
    }

    private void OnPlay()
    {
        _panelMenu.SetActive(false);
        _panelHUD.SetActive(true);
        Dispatcher.Send(Event.ON_START);
    }
    
    private void OnOptions()
    {
        _panelMenu.SetActive(false);
        _panelOptions.SetActive(true);
    }
    
    private void OnExit()
    {
        Application.Quit();
    }
    
    private void OnBackToMenu()
    {
        _panelOptions.SetActive(false);
        _panelWin.SetActive(false);
        _panelLose.SetActive(false);
        _panelMenu.SetActive(true);
    }

    private void OnScoreAdd()
    {
        SetTextScore(_scoreHUD);
    }
    
    private void OnSetCountBall()
    {
        _countBall.text = $"COUNT BALL: {GameData.instance.CountBall}";
    }

    private void OnWin()
    {
        _panelHUD.SetActive(false);
        _panelWin.SetActive(true);
        SetTextScore(_scoreWin);
    }
    
    private void OnLose()
    {
        _panelHUD.SetActive(false);
        _panelLose.SetActive(true);
        SetTextScore(_scoreLose);
    }

    private void OnAdvertisement()
    {
        // Реклама
    }

    private void OnRestart()
    {
        _panelWin.SetActive(false);
        _panelLose.SetActive(false);
        _panelHUD.SetActive(true);
        OnSetCountBall();
        SetTextScore(_scoreHUD);
        Dispatcher.Send(Event.ON_RESTART);
    }

    private void OnNextLevel()
    {
        _panelWin.SetActive(false);
        _panelHUD.SetActive(true);
        OnSetCountBall();
        Dispatcher.Send(Event.ON_NEXT_LEVEL);
    }

    private void SetTextScore(Text t)
    {
        t.text = $"SCORE: {GameData.instance.Score}";
    }

    private void OnDestroy()
    {
        Dispatcher.OnBrickDestroy -= OnScoreAdd;
        Dispatcher.OnWin -= OnWin;
        Dispatcher.OnLose -= OnLose;
        Dispatcher.OnSetCountBall -= OnSetCountBall;
    }
}
