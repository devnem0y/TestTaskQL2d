using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptions : Widget
{
    [SerializeField] private Button _btnBackToMenu;
    [SerializeField] private Button _btnСomplexityLeft;
    [SerializeField] private Button _btnСomplexityRight;

    [SerializeField] private Text _complexityLabel;
    private string[] _complexityLabels = {"EASY", "NORMAL", "HARD"};
    private int _complexityCurrentIndex = 1;
    
    #region ButtonEvents
    
    public event Action OnBackToMenuClick;

    #endregion

    private void Awake()
    {
        _btnBackToMenu.onClick.AddListener(OnBackToMenu);
        _btnСomplexityLeft.onClick.AddListener(OnСomplexityLeft);
        _btnСomplexityRight.onClick.AddListener(OnСomplexityRight);
    }
    
    private void OnBackToMenu()
    {
        OnBackToMenuClick?.Invoke();
        Hide();
        AudioManager.instance.PlaySound("button_click");
    }
    
    private void OnСomplexityLeft()
    {
        AudioManager.instance.PlaySound("button_click");
        _complexityCurrentIndex--;
        SetСomplexity();
    }
    
    private void OnСomplexityRight()
    {
        AudioManager.instance.PlaySound("button_click");
        _complexityCurrentIndex++;
        SetСomplexity();
    }

    public void SetСomplexity()
    {
        _complexityLabel.text = _complexityLabels[_complexityCurrentIndex];
        Dispatcher.Send(Event.ON_COMPLEXITY_CHANGE, _complexityCurrentIndex);
        _btnСomplexityLeft.interactable = _complexityCurrentIndex != 0;
        _btnСomplexityRight.interactable = _complexityCurrentIndex != _complexityLabels.Length - 1;
    }
}
