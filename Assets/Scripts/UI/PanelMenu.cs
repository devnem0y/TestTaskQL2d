using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : Widget
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnOptions;
    [SerializeField] private Button _btnExit;
    
    #region ButtonEvents
    
    public event Action OnPlayClick;
    public event Action OnOptionsClick;
    public event Action OnExitClick;

    #endregion

    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnPlay);
        _btnOptions.onClick.AddListener(OnOptions);
        _btnExit.onClick.AddListener(OnExit);
    }

    private void OnPlay()
    {
        OnPlayClick?.Invoke();
        Hide();
        AudioManager.instance.PlaySound("button_click");
        Dispatcher.Send(Event.ON_START);
    }
    
    private void OnOptions()
    {
        OnOptionsClick?.Invoke();
        Hide();
        AudioManager.instance.PlaySound("button_click");
    }
    
    private void OnExit()
    {
        OnExitClick?.Invoke();
        AudioManager.instance.PlaySound("button_click");
        Application.Quit();
    }
}
