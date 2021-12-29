using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private PanelMenu _panelMenu;
    [SerializeField] private PanelOptions _panelOptions;
    [SerializeField] private PanelWin _panelWin;
    [SerializeField] private PanelLose _panelLose;
    [SerializeField] private PanelHUD _panelHUD;

    private void Awake()
    {
        _panelMenu.OnPlayClick += OnPlay;
        _panelMenu.OnOptionsClick += OnOptions;
        _panelOptions.OnBackToMenuClick += OnBackToMenu;
        _panelWin.OnBackToMenuClick += OnBackToMenu;
        _panelLose.OnBackToMenuClick += OnBackToMenu;
        _panelWin.OnRestartClick += OnRestart;
        _panelLose.OnRestartClick += OnRestart;
        _panelWin.OnNextLevelClick += OnNextLevel;

        Dispatcher.OnWin += OnWin;
        Dispatcher.OnLose += OnLose;
    }

    private void Start()
    {
        _panelOptions.SetСomplexity();
    }

    private void OnPlay()
    {
        Dispatcher.Send(Event.ON_START);
        _panelHUD.Show();
    }
    
    private void OnOptions()
    {
        _panelOptions.Show();
    }

    private void OnBackToMenu()
    {
        _panelMenu.Show();
    }

    private void OnWin()
    {
        _panelHUD.Hide();
        _panelWin.Show();
    }
    
    private void OnLose()
    {
        _panelHUD.Hide();
        _panelLose.Show();
    }

    private void OnRestart()
    {
        Dispatcher.Send(Event.ON_RESTART);
        _panelHUD.Show();
    }

    private void OnNextLevel()
    {
        Dispatcher.Send(Event.ON_NEXT_LEVEL);
        _panelHUD.Show();
    }

    private void OnDestroy()
    {
        _panelMenu.OnPlayClick -= OnPlay;
        _panelMenu.OnOptionsClick -= OnOptions;
        _panelOptions.OnBackToMenuClick -= OnBackToMenu;
        _panelWin.OnBackToMenuClick -= OnBackToMenu;
        _panelLose.OnBackToMenuClick -= OnBackToMenu;
        _panelWin.OnRestartClick -= OnRestart;
        _panelLose.OnRestartClick -= OnRestart;
        _panelWin.OnNextLevelClick -= OnNextLevel;
        
        Dispatcher.OnWin -= OnWin;
        Dispatcher.OnLose -= OnLose;
    }
}
