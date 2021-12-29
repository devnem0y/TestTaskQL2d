using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelLose : Widget
{
    [SerializeField] private Text _score;
    [SerializeField] private Button _btnBackToMenu;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnAdvertisement;

    #region ButtonEvents
    
    public event Action OnBackToMenuClick;
    public event Action OnRestartClick;
    public event Action OnAdvertisementClick;

    #endregion

    private void Awake()
    {
        _btnBackToMenu.onClick.AddListener(OnBackToMenu);
        _btnRestart.onClick.AddListener(OnRestart);
        _btnAdvertisement.onClick.AddListener(OnAdvertisement);
    }
    
    public override void Show()
    {
        base.Show();
        _score.text = $"SCORE: {GameData.instance.Score}";
    }
    
    private void OnBackToMenu()
    {
        OnBackToMenuClick?.Invoke();
        Hide();
        AudioManager.instance.PlaySound("button_click");
    }
    
    private void OnRestart()
    {
        OnRestartClick?.Invoke();
        Hide();
        AudioManager.instance.PlaySound("button_click");
    }
    
    private void OnAdvertisement()
    {
        OnAdvertisementClick?.Invoke();
        AudioManager.instance.PlaySound("button_click");
        // Реклама
    }
}
